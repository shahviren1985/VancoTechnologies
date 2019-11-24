using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public enum ReporIdEnum
    {
        [Description("Committee: Event")]
        R1 = 1,
        [Description("Departmental: Guest Lectures/Talks")]
        R2 = 2,
        [Description("Departmental: Visits")]
        R3 = 3,
        [Description("Departmental: Seminars /Workshops/ Training/ Conference for students")]
        R4 = 4,
        [Description("Departmental: Students Activities at College/ Inter-college/University Level")]
        R5 = 5,
        [Description("Departmental: Collaborations/ MOUs With University, Colleges, Research Bodies, Organizations, Industries, NGOs Etc")]
        R6 = 6,
        [Description("Departmental: In House Collaborations")]
        R7 = 7,
        [Description("Departmental: International/ national/ state level conferences, seminars, organized by the department")]
        R8 = 8,
        [Description("Individual: ARTICLES IN JOURNALS")]
        R9 = 9,
        [Description("Individual: PROCEEDINGS IN CONFERENCE/SEMINAR VOLUMES")]
        R10 = 10,
        [Description("Individual: CHAPTER IN BOOKS")]
        R11 = 11,
        [Description("Individual: BOOKS- EDITED/ WRITTEN ETC")]
        R12 = 12,
        [Description("Individual: ARTICLES/  TRANSALATIONS/ CREATIVE EXPRESSIONS")]
        R13 = 13,
        [Description("Individual: PAPER PRESENTATIONS")]
        R14 = 14,
        [Description("Individual: Training  Program Attended /Organized By Faculty Members : (less than six days )")]
        R15 = 15,
        [Description("Individual: Seminars  / Conference  Attended /Organized By Faculty Members : (less than six days )")]
        R16 = 16,
        [Description("Individual: Faculty Development Programme: ( Six and More Than Six  Days Programme To Be Included Including Refreshers/ Orientation  Course )")]
        R17 = 17,
        [Description("Individual: Post Graduate Teaching")]
        R18 = 18,
        [Description("Individual: Honours/ Awards Received by Faculty Member")]
        R19 = 19,
        [Description("Peak Performers: Prize Winners at International/ National / Zonal Level")]
        R20 = 20,
        [Description("Peak Performers: Prize Winners at Inter-Collegiate level (State/Regional) ")]
        R21 = 21,
        [Description("Peak Performers: Participation at International, National, Zonal Level(Participation Certificate is required)")]
        R22 = 22,
        [Description("Peak Performers: Participation at Intercollegiate Level State/Regional (Participation Certificate is required)")]
        R23 = 23,
        [Description("Peak Performers: Prize Winners at College Level")]
        R24 = 24,
        [Description("NAAC Requirements: EditedBookDetails")]
        R25 = 25,
        [Description("NAAC Requirements: ExtensionOutReachProgramDetails")]
        R26 = 26,
        [Description("NAAC Requirements: ActivityDetails")]
        R27 = 27,
        [Description("NAAC Requirements: LinkageDetails")]
        R28 = 28,
        [Description("NAAC Requirements: MOUDetails")]
        R29 = 29,
        [Description("NAAC Requirements: DevelopmentSchemeDetails")]
        R30 = 30
    }
}