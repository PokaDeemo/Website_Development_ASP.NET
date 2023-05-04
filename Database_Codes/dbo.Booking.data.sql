SET IDENTITY_INSERT [dbo].[Booking] ON
INSERT INTO [dbo].[Booking] ([BookingID], [BookingDate], [TimeSlot], [Department], [DoctorName], [DoctorEmail], [AccountID], [PatientName], [PatientEmail], [BookingStatus]) VALUES (8, N'2022-11-26', N'13:30:00', N'Clinic Services', N'Dr.Rachel', N'doctortest@yahoo.com', 18, N'Patient05', N'patienttest5@gmail.com', N'Confirm')
INSERT INTO [dbo].[Booking] ([BookingID], [BookingDate], [TimeSlot], [Department], [DoctorName], [DoctorEmail], [AccountID], [PatientName], [PatientEmail], [BookingStatus]) VALUES (12, N'2022-11-16', N'17:13:00', N'Clinic Services', N'Dr.Daniel', N'doctortest2@yahoo.com', 19, N'Patient01', N'patienttest@gmail.com', N'Pending')
INSERT INTO [dbo].[Booking] ([BookingID], [BookingDate], [TimeSlot], [Department], [DoctorName], [DoctorEmail], [AccountID], [PatientName], [PatientEmail], [BookingStatus]) VALUES (13, N'2022-11-19', N'15:23:00', N'Clinic Services', N'Dr.Rachel', N'doctortest@yahoo.com', 18, N'Patient06', N'patienttest6@gmail.com', N'Pending')
SET IDENTITY_INSERT [dbo].[Booking] OFF