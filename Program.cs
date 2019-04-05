using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrimeAnalyzer {
    class Program {
        const int CSVLineLength = 11;
        const string CSVHeaders = "Year,Population,Violent Crime,Murder,Rape,Robbery,Aggravated Assault,Property Crime,Burglary,Theft,Motor Vehicle Theft";

        static void Main(string[] args) {
            if (args.Length == 2) {
                try {
                    List<CrimeStats> crimeStatsList = ReadCrimeData(args[0]);
                    GenerateCrimeReport(crimeStatsList, args[1]);
                    Console.WriteLine("Successfully created Crime Report: {0}", args[1]);
                } catch (Exception e) {
                    if (e is InvalidCSVHeadersException || e is InvalidCSVLineException || e is InvalidCSVValueException || e is CSVFileReadException || e is CrimeReportFileWriteException || e is CrimeReportEmptyDataException) {
                        Console.WriteLine(e.Message);
                    } else {
                        Console.WriteLine("An unexpected error occured: {0}", e.Message);
                    }
                }
            } else {
                Console.WriteLine("CrimeAnalyzer <Crime CSV path> <Report path>");
            }
            Console.ReadLine();
        }

        static List<CrimeStats> ReadCrimeData(string filePath) {
            StreamReader sr = null;
            try {
                sr = new StreamReader(filePath);
                string headers = sr.ReadLine();
                if (headers == CSVHeaders) {
                    List<CrimeStats> statsList = new List<CrimeStats>();
                    while (!sr.EndOfStream) {
                        string[] values = sr.ReadLine().Split(',');
                        if (values.Length == CSVLineLength) {
                            int[] parsedValues = new int[CSVLineLength];
                            for (int i = 0; i < values.Length; i++) {
                                if (int.TryParse(values[i], out int result)) {
                                    parsedValues[i] = result;
                                } else {
                                    throw new InvalidCSVValueException(string.Format("Value of \"{0}\" could not be determined in Row {1}, Column {2}", values[i], statsList.Count + 1, i));
                                }
                            }
                            statsList.Add(new CrimeStats()
                            {
                                Year = parsedValues[0],
                                Population = parsedValues[1],
                                ViolentCrime = parsedValues[2],
                                Murder = parsedValues[3],
                                Rape = parsedValues[4],
                                Robbery = parsedValues[5],
                                AggravatedAssault = parsedValues[6],
                                PropertyCrime = parsedValues[7],
                                Burglary = parsedValues[8],
                                Theft = parsedValues[9],
                                MotorVehicleTheft = parsedValues[10]
                            });
                        } else {
                            throw new InvalidCSVLineException(string.Format("Row {0} contains {1} values. It should contain {2}.", statsList.Count + 1, values.Length, CSVLineLength));
                        }
                    }
                    return statsList;
                } else {
                    throw new InvalidCSVHeadersException(string.Format("The headers in the provided CSV file are not the same as the expected headers \"{0}\"", CSVHeaders));
                }
            } catch (Exception e) {
                if (e is FileNotFoundException || e is DirectoryNotFoundException || e is IOException || e is ArgumentException) {
                    throw new CSVFileReadException(string.Format("Could not find the CSV file at the specified path: \"{0}\"", filePath));
                } else {
                    throw e;
                }
            } finally {
                if (sr != null) {
                    sr.Close();
                }
            }
        }

        static void GenerateCrimeReport(List<CrimeStats> CrimeStatsList, string filePath) {
            if (CrimeStatsList.Count == 0) {
                throw new CrimeReportEmptyDataException("Data provided for the Crime Report is empty. Please check your CSV file.");
            }

            StreamWriter sw = null;
            try {
                sw = new StreamWriter(filePath);

                sw.WriteLine("+= Crime Analyzer Report =+\n");

                IEnumerable<int> years = from CrimeStats in CrimeStatsList select CrimeStats.Year;
                int start = years.Min();
                int end = years.Max();
                int span = end - start + 1;
                sw.WriteLine("Period: {0}-{1} ({2} year{3})\n", start, end, span, span > 1 ? "s" : "");

                string csv = "";
                int index = 0;
                IEnumerable<int> murderYears = from CrimeStats in CrimeStatsList where CrimeStats.Murder < 15000 select CrimeStats.Year;
                foreach (int year in murderYears) {
                    csv += string.Format("{0}{1}", year, index < (murderYears.Count() - 1) ? ", " : "");
                    ++index;
                }
                sw.WriteLine("Years murders per year < 15000: {0}", csv);

                csv = "";
                index = 0;
                IEnumerable<CrimeStats> robberyStats = from CrimeStats in CrimeStatsList where CrimeStats.Robbery > 500000 select CrimeStats;
                foreach (CrimeStats stats in robberyStats) {
                    csv += string.Format("{0} = {1}{2}", stats.Year, stats.Robbery, index < (robberyStats.Count() - 1) ? ", " : "");
                    ++index;
                }
                sw.WriteLine("Robberies per year > 500000: {0}", csv);

                CrimeStats cpcYear = (from CrimeStats in CrimeStatsList where CrimeStats.Year == 2010 select CrimeStats).FirstOrDefault();
                string cpcText = cpcYear == null ? "No data to calculate crime per capita in 2010" : ((double)cpcYear.ViolentCrime / cpcYear.Population).ToString();
                sw.WriteLine("Violent crime per capita rate (2010): {0}", cpcText);

                double averageMurders = (from CrimeStats in CrimeStatsList select CrimeStats.Murder).Average();
                sw.WriteLine("Average murders per year (all years): {0}", averageMurders);

                averageMurders = (from CrimeStats in CrimeStatsList where (CrimeStats.Year >= 1994 && CrimeStats.Year <= 1997) select CrimeStats.Murder).Average();
                sw.WriteLine("Average murders per year (1994-1997): {0}", averageMurders);

                averageMurders = (from CrimeStats in CrimeStatsList where (CrimeStats.Year >= 2010 && CrimeStats.Year <= 2014) select CrimeStats.Murder).Average();
                sw.WriteLine("Average murders per year (2010-2014): {0}", averageMurders);

                IEnumerable<int> thefts = (from CrimeStats in CrimeStatsList where (CrimeStats.Year >= 1999 && CrimeStats.Year <= 2004) select CrimeStats.Theft);
                sw.WriteLine("Minimum Therfts per year (1999-2004): {0}", thefts.Min());
                sw.WriteLine("Maximum Therfts per year (1999-2004): {0}", thefts.Max());

                int maxMotorTheftsYear = (from CrimeStats in CrimeStatsList let maxMotorTheft = CrimeStatsList.Max(stats => stats.MotorVehicleTheft) where CrimeStats.MotorVehicleTheft == maxMotorTheft select CrimeStats.Year).FirstOrDefault();

                sw.WriteLine("Year with the highest number of Motor Vehicle thefts: {0}", maxMotorTheftsYear);
            } catch (Exception e) {
                if (e is UnauthorizedAccessException || e is ArgumentException || e is DirectoryNotFoundException || e is PathTooLongException || e is IOException) {
                    throw new CrimeReportFileWriteException(string.Format("File at \"{0}\" is incorrect.\nPath either does not exist or is formatted incorrectly.\nPlease try again.", filePath));
                } else {
                    throw e;
                }
            } finally {
                if (sw != null) {
                    sw.Close();
                }
            }
        }
    }
}
