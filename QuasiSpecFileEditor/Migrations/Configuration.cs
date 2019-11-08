namespace QuasiSpecFileEditor.Migrations
{
    using QuasiSpecFileEditor.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuasiSpecFileEditor.DAL.SpecFileDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuasiSpecFileEditor.DAL.SpecFileDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var models = new List<Model>
            {
                new Model { CodeName = "LHA" },
                new Model { CodeName = "NHK" },
                new Model { CodeName = "LBE" }
            };
            models.ForEach(m => context.Models.AddOrUpdate(c => c.CodeName, m));
            context.SaveChanges();

            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    FieldName = "P2P07_Resistance",
                    UpperLimitDefect = "QSTRT",
                    LowerLimit = 2000,
                    LowerLimitDefect = "QSTRT",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P07_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P06_Resistance",
                    UpperLimitDefect = "QSTTRR",
                    LowerLimit = 2000,
                    LowerLimitDefect = "QSTTRR",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P06_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P05_Resistance",
                    UpperLimitDefect = "QSTTGR",
                    LowerLimit = 2000,
                    LowerLimitDefect = "QSTTGR",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P05_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P04_Resistance",
                    UpperLimitDefect = "QSTSTG",
                    LowerLimit = 7000,
                    LowerLimitDefect = "QSTSTG",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P04_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P03_Resistance",
                    UpperLimitDefect = "QSTWSN",
                    LowerLimit = 1000000,
                    LowerLimitDefect = "QSTWSN",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P03_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P02_Resistance",
                    UpperLimitDefect = "QSTMWN",
                    LowerLimit = 1000000,
                    LowerLimitDefect = "QSTMWN",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P02_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P01_Resistance",
                    UpperLimitDefect = "QSTIVC",
                    LowerLimit = 90000,
                    LowerLimitDefect = "QSTIVC",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P01_Resistance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "P2P01_Capacitance",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "P2P",
                    CSVHeaderName = "P2P01_Capacitance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTAB_CapacitanceTotal",
                    UpperLimitDefect = "QSTHC",
                    LowerLimitDefect = "QSTLC",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTAB_CapacitanceTotal",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTAB_TangentDeltaMax",
                    UpperLimit = 0.1,
                    UpperLimitDefect = "QSTHTD",
                    OpReqUpload = "1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTAB_TangentDeltaMax",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTA_Capacitance",
                    UpperLimit = 620,
                    UpperLimitDefect = "QSTHCA",
                    LowerLimit = 470,
                    LowerLimitDefect = "QSTLCA",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTA_Capacitance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTA_TangentDelta",
                    UpperLimitDefect = "QSTHTD",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTA_TangentDelta",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTB_Capacitance",
                    UpperLimit = 620,
                    UpperLimitDefect = "QSTHCB",
                    LowerLimit = 470,
                    LowerLimitDefect = "QSTLCB",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTB_Capacitance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTB_TangentDelta",
                    UpperLimitDefect = "QSTHTD",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTB_TangentDelta",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTC_Capacitance",
                    UpperLimit = 588,
                    UpperLimitDefect = "QSTHCC",
                    LowerLimit = 512,
                    LowerLimitDefect = "QSTLCC",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTC_Capacitance",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "PZTC_TangentDelta",
                    UpperLimit = 0.1,
                    UpperLimitDefect = "QSTMTD",
                    OpReqUpload = "1220,1650,1001",
                    ParamTest = "PZT",
                    CSVHeaderName = "PZTC_TangentDelta",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "Short_TG_WN",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "Short_TG_WN",
                    LowerLimitDefect = "QSTTH",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "Short_TP_RP",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "Short_TP_RP",
                    LowerLimitDefect = "QSTTL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "Short_RN_WP",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "Short_RN_WP",
                    LowerLimitDefect = "QSTRL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "RP_SP",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "R+_S+",
                    LowerLimitDefect = "QSTRS",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "TFCG_WP",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "TFCG_W+",
                    LowerLimitDefect = "QSTTW",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "TFCP_RN",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "TTFC+_R-",
                    LowerLimitDefect = "QSTTR",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "SN_TFCG",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "S-_TFCG",
                    LowerLimitDefect = "QSTST",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "WC",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "WC",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 8,
                    UpperLimitDefect = "QSTWH",
                    LowerLimit = 3.5,
                    LowerLimitDefect = "QSTWL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "TFC",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "TFC",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 160,
                    UpperLimitDefect = "QSTTH",
                    LowerLimit = 50,
                    LowerLimitDefect = "QSTTL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "ECS",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "ECS",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 300,
                    UpperLimitDefect = "QSTEH",
                    LowerLimit = 60,
                    LowerLimitDefect = "QSTEL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "RH",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "RH",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 1500,
                    UpperLimitDefect = "QSTRH1",
                    LowerLimit = 100,
                    LowerLimitDefect = "QSTRL1",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "RH2",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "RH2",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 1000,
                    UpperLimitDefect = "QSTRH2",
                    LowerLimit = 200,
                    LowerLimitDefect = "QSTRL2",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "STOC",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "STOC",
                    OpReqUpload = "1220,1650,1001",
                    UpperLimit = 50,
                    UpperLimitDefect = "QSTMOH",
                    LowerLimit = 5,
                    LowerLimitDefect = "QSTMOL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "AMP",
                    ParamTest = "QUASIFX",
                    CSVHeaderName = "AMP",
                    UpperLimitDefect = "QSTAH",
                    LowerLimitDefect = "QSTAL",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                },
                new Parameter
                {
                    FieldName = "WAFER_NGChar",
                    UpperLimit = 0.02,
                    UpperLimitDefect = "E",
                    LastModifiedBy = "7326678",
                    LastModifyDate = DateTime.Now
                }
            };
            parameters.ForEach(p => context.Parameters.AddOrUpdate(c => c.FieldName, p));
            context.SaveChanges();

            var alarms = new List<Alarm>
            {
                new Alarm
                {
                    AlarmName = "P2P_YIELD",
                    Comment = "Hold"
                },
                new Alarm
                {
                    AlarmName = "PZT_YIELD",
                    Comment = "Hold"
                },
                new Alarm
                {
                    AlarmName = "QUASIFX_YIELD",
                    Comment = "Hold"
                },
                new Alarm
                {
                    AlarmName = "OVERALL_YIELD",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "QSTIVC",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "CapA_Low",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "CapB_Low",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "CapC_Low",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "WC_High",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "RH_High",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "RH2_High",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "RH_Low",
                    Comment = "IssuePIS"
                },
                new Alarm
                {
                    AlarmName = "RH2_Low",
                    Comment = "IssuePIS"
                },
            };
            alarms.ForEach(a => context.Alarms.AddOrUpdate(c => c.AlarmName, a));
            context.SaveChanges();

            var parts = new List<Part>
            {
                new Part { PN = "0F25336", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32587", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32588", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34477", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34478", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34517", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34518", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34527", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34528", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32525", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32526", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32589", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32590", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34479", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34480", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34519", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34520", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34529", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F34530", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32505", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
                new Part { PN = "0F32506", ModelID = models.Single(s => s.CodeName == "LHA").ModelID },
            };
            parts.ForEach(p => context.Parts.AddOrUpdate(c => c.PN, p));
            context.SaveChanges();
        }
    }
}
