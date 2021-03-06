USE [Volunteer]
GO
/****** Object:  View [dbo].[IndividualUserStatistics]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IndividualUserStatistics]
AS
SELECT        Created.Month, Months.MonthName, Created.Year, Created.UserID, Created.Count AS HostedCount, ISNULL(Volunteer.Count, 0) AS VolunteerCount, ISNULL(Volunteer.WithdrawnCount, 0) AS WithdrawnCount, 
                         ISNULL(Volunteer.RejectedCount, 0) AS RejectedCount, ISNULL(Volunteer.ConfirmedCount, 0) AS ConfirmedCount, ISNULL(Volunteer.PendingConfirmationCount, 0) AS PendingConfirmationCount
FROM            (SELECT        MONTH(Created) AS Month, YEAR(Created) AS Year, COUNT(*) AS Count, HostID AS UserID
                          FROM            dbo.Event AS e
                          GROUP BY YEAR(Created), MONTH(Created), HostID) AS Created LEFT OUTER JOIN
                             (SELECT        MONTH(e.Date) AS Month, YEAR(e.Date) AS Year, COUNT(*) AS Count, SUM(CASE WHEN v.Withdrawn = 1 THEN 1 ELSE 0 END) AS WithdrawnCount, 
                                                         SUM(CASE WHEN v.Rejected = 1 THEN 1 ELSE 0 END) AS RejectedCount, SUM(CASE WHEN v.Confirmed = 1 THEN 1 ELSE 0 END) AS ConfirmedCount, SUM(CASE WHEN v.Accepted = 1 AND 
                                                         v.Rejected <> 1 AND v.Withdrawn <> 1 AND v.Confirmed <> 1 THEN 1 ELSE 0 END) AS PendingConfirmationCount, v.VolunteerID AS UserID
                               FROM            dbo.Event AS e INNER JOIN
                                                         dbo.Volunteer AS v ON v.EventID = e.ID
                               GROUP BY YEAR(e.Date), MONTH(e.Date), v.VolunteerID) AS Volunteer ON Volunteer.Month = Created.Month AND Volunteer.Year = Created.Year AND Volunteer.UserID = Created.UserID LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Created.Month

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Created"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Volunteer"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 483
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 6
               Left = 521
               Bottom = 102
               Right = 691
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IndividualUserStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IndividualUserStatistics'
GO
