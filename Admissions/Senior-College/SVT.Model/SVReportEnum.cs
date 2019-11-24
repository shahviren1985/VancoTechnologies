using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public enum ReporIdEnum
    {
        #region Round 1 Enum
        [Description("Developmental Counselling: SVTOpenInternalRound1")]
        R1 = 1,
        [Description("Developmental Counselling:SVTReservedInternalRound1")]
        R2 = 2,
        [Description("Developmental Counselling:ExternalOpenRound1")]
        R3 = 3,
        [Description("Developmental Counselling:ExternalReservedRound1")]
        R4 = 4,

        [Description("Early Childhood care & Education: SVTOpenInternalRound1")]
        R5 = 5,
        [Description("Early Childhood care & Education:SVTReservedInternalRound1")]
        R6 = 6,
        [Description("Early Childhood care & Education:ExternalOpenRound1")]
        R7 = 7,
        [Description("Early Childhood care & Education:ExternalReservedRound1")]
        R8 = 8,

        [Description("Interior Design & Resource Management: SVTOpenInternalRound1")]
        R9 = 9,
        [Description("Interior Design & Resource Management:SVTReservedInternalRound1")]
        R10 = 10,
        [Description("Interior Design & Resource Management:ExternalOpenRound1")]
        R11 = 11,
        [Description("Interior Design & Resource Management:ExternalReservedRound1")]
        R12 = 12,

        [Description("Textiles & Apparel Designing: SVTOpenInternalRound1")]
        R13 = 13,
        [Description("Textiles & Apparel Designing:SVTReservedInternalRound1")]
        R14 = 14,
        [Description("Textiles & Apparel Designing:ExternalOpenRound1")]
        R15 = 15,
        [Description("Textiles & Apparel Designing:ExternalReservedRound1")]
        R16 = 16,

        [Description("Hospitality & Tourism Management: SVTOpenInternalRound1")]
        R17 = 17,
        [Description("Hospitality & Tourism Management:SVTReservedInternalRound1")]
        R18 = 18,
        [Description("Hospitality & Tourism Management:ExternalOpenRound1")]
        R19 = 19,
        [Description("Hospitality & Tourism Management:ExternalReservedRound1")]
        R20 = 20,

        [Description("Mass Communication & Extension: SVTOpenInternalRound1")]
        R21 = 21,
        [Description("Mass Communication & Extension:SVTReservedInternalRound1")]
        R22 = 22,
        [Description("Mass Communication & Extension:ExternalOpenRound1")]
        R23 = 23,
        [Description("Mass Communication & Extension:ExternalReservedRound1")]
        R24 = 24,

        [Description("Food, Nutrition and Dietetics: SVTOpenInternalRound1")]
        R25 = 25,
        [Description("Food, Nutrition and Dietetics:SVTReservedInternalRound1")]
        R26 = 26,
        [Description("Food, Nutrition and Dietetics:ExternalOpenRound1")]
        R27 = 27,
        [Description("Food, Nutrition and Dietetics:ExternalReservedRound1")]
        R28 = 28,
        #endregion

        #region Round 2 Enum
        [Description("Developmental Counselling: SVTOpenInternalRound2")]
        R29 = 29,
        [Description("Developmental Counselling:SVTReservedInternalRound2")]
        R30 = 30,
        [Description("Developmental Counselling:ExternalOpenRound2")]
        R31 = 31,
        [Description("Developmental Counselling:ExternalReservedRound2")]
        R32 = 32,

        [Description("Early Childhood care & Education: SVTOpenInternalRound2")]
        R33 = 33,
        [Description("Early Childhood care & Education:SVTReservedInternalRound2")]
        R34 = 34,
        [Description("Early Childhood care & Education:ExternalOpenRound2")]
        R35 = 35,
        [Description("Early Childhood care & Education:ExternalReservedRound2")]
        R36 = 36,

        [Description("Interior Design & Resource Management: SVTOpenInternalRound2")]
        R37 = 37,
        [Description("Interior Design & Resource Management:SVTReservedInternalRound2")]
        R38 = 38,
        [Description("Interior Design & Resource Management:ExternalOpenRound2")]
        R39 = 39,
        [Description("Interior Design & Resource Management:ExternalReservedRound2")]
        R40 = 40,

        [Description("Textiles & Apparel Designing: SVTOpenInternalRound2")]
        R41 = 41,
        [Description("Textiles & Apparel Designing:SVTReservedInternalRound2")]
        R42 = 42,
        [Description("Textiles & Apparel Designing:ExternalOpenRound2")]
        R43 = 43,
        [Description("Textiles & Apparel Designing:ExternalReservedRound2")]
        R44 = 44,

        [Description("Hospitality & Tourism Management: SVTOpenInternalRound2")]
        R45 = 45,
        [Description("Hospitality & Tourism Management:SVTReservedInternalRound2")]
        R46 = 46,
        [Description("Hospitality & Tourism Management:ExternalOpenRound2")]
        R47 = 47,
        [Description("Hospitality & Tourism Management:ExternalReservedRound2")]
        R48 = 48,

        [Description("Mass Communication & Extension: SVTOpenInternalRound2")]
        R49 = 49,
        [Description("Mass Communication & Extension:SVTReservedInternalRound2")]
        R50 = 50,
        [Description("Mass Communication & Extension:ExternalOpenRound2")]
        R51 = 51,
        [Description("Mass Communication & Extension:ExternalReservedRound2")]
        R52 = 52,

        [Description("Food, Nutrition and Dietetics: SVTOpenInternalRound2")]
        R53 = 53,
        [Description("Food, Nutrition and Dietetics:SVTReservedInternalRound2")]
        R54 = 54,
        [Description("Food, Nutrition and Dietetics:ExternalOpenRound2")]
        R55 = 55,
        [Description("Food, Nutrition and Dietetics:ExternalReservedRound2")]
        R56 = 56,
        #endregion

        #region Round 3 Enum
        [Description("Developmental Counselling: SVTOpenInternalRound3")]
        R57 = 57,
        [Description("Developmental Counselling:SVTReservedInternalRound3")]
        R58 = 58,
        [Description("Developmental Counselling:ExternalOpenRound3")]
        R59 = 59,
        [Description("Developmental Counselling:ExternalReservedRound3")]
        R60 = 60,

        [Description("Early Childhood care & Education: SVTOpenInternalRound3")]
        R61 = 61,
        [Description("Early Childhood care & Education:SVTReservedInternalRound3")]
        R62 = 62,
        [Description("Early Childhood care & Education:ExternalOpenRound3")]
        R63 = 63,
        [Description("Early Childhood care & Education:ExternalReservedRound3")]
        R64 = 64,

        [Description("Interior Design & Resource Management: SVTOpenInternalRound3")]
        R65 = 65,
        [Description("Interior Design & Resource Management:SVTReservedInternalRound3")]
        R66 = 66,
        [Description("Interior Design & Resource Management:ExternalOpenRound3")]
        R67 = 67,
        [Description("Interior Design & Resource Management:ExternalReservedRound3")]
        R68 = 68,

        [Description("Textiles & Apparel Designing: SVTOpenInternalRound3")]
        R69 = 69,
        [Description("Textiles & Apparel Designing:SVTReservedInternalRound3")]
        R70 = 70,
        [Description("Textiles & Apparel Designing:ExternalOpenRound3")]
        R71 = 71,
        [Description("Textiles & Apparel Designing:ExternalReservedRound3")]
        R72 = 72,

        [Description("Hospitality & Tourism Management: SVTOpenInternalRound3")]
        R73 = 73,
        [Description("Hospitality & Tourism Management:SVTReservedInternalRound3")]
        R74 = 74,
        [Description("Hospitality & Tourism Management:ExternalOpenRound3")]
        R75 = 75,
        [Description("Hospitality & Tourism Management:ExternalReservedRound3")]
        R76 = 76,

        [Description("Mass Communication & Extension: SVTOpenInternalRound3")]
        R77 = 77,
        [Description("Mass Communication & Extension:SVTReservedInternalRound3")]
        R78 = 78,
        [Description("Mass Communication & Extension:ExternalOpenRound3")]
        R79 = 79,
        [Description("Mass Communication & Extension:ExternalReservedRound3")]
        R80 = 80,

        [Description("Food, Nutrition and Dietetics: SVTOpenInternalRound3")]
        R81 = 81,
        [Description("Food, Nutrition and Dietetics:SVTReservedInternalRound3")]
        R82 = 82,
        [Description("Food, Nutrition and Dietetics:ExternalOpenRound3")]
        R83 = 83,
        [Description("Food, Nutrition and Dietetics:ExternalReservedRound3")]
        R84 = 84
        #endregion

    }
}
