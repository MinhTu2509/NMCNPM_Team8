--Mọi thông tin đều chỉ mang tính chất minh hoạ, hầu như không đúng trong thực tế
--Các bước : sửa trigger TRG_VEMAYBAY => insert Tham số,sân bay, hành khách=> thêm chuyến bay, hạng vé, vé máy bay => sửa trigger TRG_VEMAYBAY 
USE BANVECHUYENBAY
GO

ALTER TRIGGER [dbo].[TRG_VEMAYBAY] ON [dbo].[VEMAYBAY]
FOR INSERT
AS
BEGIN
	DECLARE @COUNT2 INT
	DECLARE @SLGHE INT
	SELECT @SLGHE = COUNT(*) FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	SELECT @COUNT2 = COUNT(*)
	FROM 
	(
		SELECT inserted.MaChuyenBay, inserted.MaHangVe, COUNT(*) AS SoLuongGhe FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	) AS K, SOLUONGVE
	WHERE K.MaChuyenBay = SOLUONGVE.MaChuyenBay AND K.MaHangVe = SOLUONGVE.MaHangVe AND K.SoLuongGhe > SOLUONGVE.TongSoGhe - SOLUONGVE.SoGheDaDat
	IF(@COUNT2>0)
	BEGIN
		PRINT(@SLGHE)
		PRINT(N'ERROR: Không đủ ghế')
		ROLLBACK TRANSACTION
	END


	UPDATE SOLUONGVE SET SoGheDaDat = SoGheDaDat + 1
	WHERE SOLUONGVE.MaChuyenBay IN (SELECT MaChuyenBay FROM inserted )
	AND SOLUONGVE.MaHangVe IN (SELECT MaHangVe FROM inserted WHERE inserted.MaChuyenBay = SOLUONGVE.MaChuyenBay)

END 
go
insert into THAMSO values(0,2,30,10,20,0,24,23);
insert into SANBAY values('AP0001',N'Nội Bài');
insert into SANBAY values('AP0002',N'Tân Sơn Nhất');
insert into SANBAY values('AP0003',N'Cát Bi');
insert into SANBAY values('AP0004',N'Vinh');
insert into SANBAY values('AP0005',N'Phú Bài');
insert into SANBAY values('AP0006',N'Đà Nẵng');
insert into SANBAY values('AP0007',N'Cam Ranh');
insert into SANBAY values('AP0008',N'Cần Thơ');
insert into SANBAY values('AP0009',N'Phú Quốc');
insert into SANBAY values('AP0010',N'Vân Đồn');

insert into HANGVE values('HV0001',N'Hạng thương gia',1.05);
insert into HANGVE values('HV0002',N'Hang phổ thông',1.5);
insert into HANGVE values('HV0003',N'Hạng Skyboss',2);

insert into HANHKHACH values('21521619',N'TRAN TINH MINH TU','0943063043');
insert into HANHKHACH values('21520001', 'NGUYEN DANG HOANG HA', '0879038473');
insert into HANHKHACH values('21520002', 'LE NGUYEN DIEM QUYNH', '0844573706');
insert into HANHKHACH values('21520003', 'NGUYEN THI MINH ANH', '0844573709');
insert into HANHKHACH values('21520004', 'NGUYEN VAN A', '0844573777');
insert into HANHKHACH values('21520005', 'TRAN THI B','096031201');
insert into HANHKHACH values('21520006', 'VO ANH C', '19001082');
insert into HANHKHACH values('21520007', 'LE THI D', '19001083');
insert into HANHKHACH values('21520008', 'HUYNH THI E', '19001082');
insert into HANHKHACH values('21520009', 'DOAN VAN F','0168923268');
insert into HANHKHACH values('21520010', 'LUFFY', '123456789');
insert into HANHKHACH values('21520011', 'ZORO', '135827242');
insert into HANHKHACH values('21520012', 'SANJI', '11253413461');
insert into HANHKHACH values('21520013', 'ROBIN', '123456789');
insert into HANHKHACH values('21520014', 'CHOPPER', '135827242');
insert into HANHKHACH values('21520015', 'USOPP','0123262677');

insert into CHUYENBAY values('MH-202109-0667','AP0005','AP0001',100000,'2022-6-9 10:30:00',30)
insert into SOLUONGVE values('MH-202109-0667','HV0001',8,0)
insert into SOLUONGVE values('MH-202109-0667','HV0002',8,0)
insert into SOLUONGVE values('MH-202109-0667','HV0003',3,0)
insert into VEMAYBAY values('VE0000','MH-202109-0667','21521619','HV0001',110000,'Da thanh toan','2022-6-8 00:00:00')
insert into VEMAYBAY values('VE0001','MH-202109-0667','21520001','HV0002',150000,'Da thanh toan','2022-6-3 00:00:00')
insert into VEMAYBAY values('VE0003','MH-202109-0667','21521619','HV0001',110000,'Da thanh toan','2022-6-2 00:00:00')
insert into VEMAYBAY values('VE0004','MH-202109-0667','21521619','HV0003',200000,'Da thanh toan','2022-6-7 00:00:00')

insert into CHUYENBAY values('MH-202109-0678','AP0010','AP0004',100000,'2022-7-10 13:30:00',31)
insert into SOLUONGVE values('MH-202109-0678','HV0002',10,0)
insert into SOLUONGVE values('MH-202109-0678','HV0003',5,0)
insert into VEMAYBAY values('VE0005','MH-202109-0678','21520002','HV0002',150000,'Da thanh toan','2022-6-29 00:00:00')
insert into VEMAYBAY values('VE0006','MH-202109-0678','21520002','HV0003',200000,'Da thanh toan','2022-6-30 00:00:00')
insert into VEMAYBAY values('VE0007','MH-202109-0678','21520002','HV0002',150000,'Da thanh toan','2022-7-7 00:00:00')
insert into VEMAYBAY values('VE0008','MH-202109-0678','21520002','HV0003',200000,'Da thanh toan','2022-7-3 00:00:00')

insert into CHUYENBAY values('MH-202110-0679','AP0001','AP0003',100000,'2022-8-30 8:30:00',45) 
insert into SOLUONGVE values('MH-202110-0679','HV0003',15,0)
insert into VEMAYBAY values('VE0009','MH-202110-0679','21520003','HV0003',200000,'Da thanh toan','2022-8-20 00:00:00')
insert into VEMAYBAY values('VE0010','MH-202110-0679','21520003','HV0003',200000,'Da thanh toan','2022-8-29 00:00:00')
insert into VEMAYBAY values('VE0011','MH-202110-0679','21520003','HV0003',200000,'Da thanh toan','2022-8-15 00:00:00')
insert into VEMAYBAY values('VE0012','MH-202110-0679','21520004','HV0003',200000,'Da thanh toan','2022-8-28 00:00:00')

insert into CHUYENBAY values('MH-202110-0680','AP0007','AP0002',200000,'2022-9-3 12:34:00',30)
insert into SOLUONGVE values('MH-202110-0680','HV0001',20,0)
insert into VEMAYBAY values('VE0013','MH-202110-0680','21520005','HV0001',220000,'Da thanh toan','2022-9-1 00:00:00')
insert into VEMAYBAY values('VE0014','MH-202110-0680','21520005','HV0001',220000,'Da thanh toan','2022-9-1 00:00:00')

insert into CHUYENBAY values('MH-202111-0681','AP0004','AP0002',120000,'2022-10-15 10:30:00',30)
insert into SOLUONGVE values('MH-202111-0681','HV0001',20,0)
insert into SOLUONGVE values('MH-202111-0681','HV0003',5,0)
insert into VEMAYBAY values('VE0015','MH-202111-0681','21520006','HV0001',132000,'Da thanh toan','2022-10-14 00:00:00')
insert into VEMAYBAY values('VE0016','MH-202111-0681','21520006','HV0003',240000,'Da thanh toan','2022-10-14 00:00:00')
insert into VEMAYBAY values('VE0017','MH-202111-0681','21520007','HV0001',132000,'Da thanh toan','2022-10-14 00:00:00')
insert into VEMAYBAY values('VE0018','MH-202111-0681','21520007','HV0003',240000,'Da thanh toan','2022-10-14 00:00:00')

insert into CHUYENBAY values('MH-202111-0682','AP0009','AP0004',100000,'2022-11-16 13:30:00',31)
insert into SOLUONGVE values('MH-202111-0682','HV0002',5,0)
insert into VEMAYBAY values('VE0019','MH-202111-0682','21520008','HV0003',150000,'Da thanh toan','2022-11-12 00:00:00')
insert into VEMAYBAY values('VE0020','MH-202111-0682','21520008','HV0003',150000,'Da thanh toan','2022-11-12 00:00:00')

insert into CHUYENBAY values('MH-202112-0683','AP0009','AP0005',100000,'2022-12-16 13:30:00',31)
insert into SOLUONGVE values('MH-202112-0683','HV0003',5,0)
insert into VEMAYBAY values('VE0021','MH-202112-0683','21520009','HV0003',200000,'Da thanh toan','2022-12-15 00:00:00')
insert into VEMAYBAY values('VE0022','MH-202112-0683','21520009','HV0003',200000,'Da thanh toan','2022-12-15 00:00:00')

insert into CHUYENBAY values('MH-202112-0686','AP0001','AP0004',150000,'2022-12-17 8:30:00',32) 
insert into SOLUONGVE values('MH-202112-0686','HV0001',10,0)
insert into SOLUONGVE values('MH-202112-0686','HV0003',5,0)
insert into VEMAYBAY values('VE0023','MH-202112-0686','21520010','HV0001',165000,'Da thanh toan','2022-12-16 00:00:00')
insert into VEMAYBAY values('VE0024','MH-202112-0686','21520011','HV0003',300000,'Da thanh toan','2022-12-16 00:00:00')
insert into VEMAYBAY values('VE0025','MH-202112-0686','21520013','HV0001',165000,'Da thanh toan','2022-12-16 00:00:00')
insert into VEMAYBAY values('VE0026','MH-202112-0686','21520013','HV0001',165000,'Da thanh toan','2022-12-16 00:00:00')

insert into CHUYENBAY values('MH-202112-0687','AP0006','AP0003',100000,'2022-12-18 12:34:00',33)
insert into SOLUONGVE values('MH-202112-0687','HV0001',10,0)
insert into SOLUONGVE values('MH-202112-0687','HV0002',5,0)
insert into VEMAYBAY values('VE0027','MH-202112-0687','21520014','HV0001',110000,'Da thanh toan','2022-12-17 00:00:00')
insert into VEMAYBAY values('VE0028','MH-202112-0687','21520015','HV0002',150000,'Da thanh toan','2022-12-17 00:00:00')
insert into VEMAYBAY values('VE0029','MH-202112-0687','21520001','HV0001',110000,'Da thanh toan','2022-12-17 00:00:00')
insert into VEMAYBAY values('VE0030','MH-202112-0687','21520002','HV0002',150000,'Da thanh toan','2022-12-17 00:00:00')

insert into CHUYENBAY values('MH-202201-0688','AP0004','AP0006',100000,'2023-1-20 12:22:00',44)
insert into SOLUONGVE values('MH-202201-0688','HV0003',5,0)
insert into VEMAYBAY values('VE0031','MH-202201-0688','21521619','HV0003',200000,'Da thanh toan','2023-1-19 00:00:00')
insert into VEMAYBAY values('VE0032','MH-202201-0688','21521619','HV0003',200000,'Da thanh toan','2023-1-19 00:00:00')

insert into CHUYENBAY values('MH-202201-0770','AP0002','AP0001',100000,'2023-1-27 20:30:00',30)
insert into SOLUONGVE values('MH-202201-0770','HV0001',9,0)
insert into SOLUONGVE values('MH-202201-0770','HV0002',9,0)
insert into VEMAYBAY values('VE0033','MH-202201-0770','21521619','HV0001',110000,'Da thanh toan','2023-1-26 00:00:00')
insert into VEMAYBAY values('VE0034','MH-202201-0770','21521619','HV0002',150000,'Da thanh toan','2023-1-27 00:00:00')
insert into VEMAYBAY values('VE0035','MH-202201-0770','21521619','HV0001',110000,'Da thanh toan','2023-1-26 00:00:00')
insert into VEMAYBAY values('VE0036','MH-202201-0770','21521619','HV0002',150000,'Da thanh toan','2023-1-27 00:00:00')

insert into CHUYENBAY values('MH-202201-0866','AP0010','AP0009',150000,'2023-1-30 20:34:00',31)
insert into SOLUONGVE values('MH-202201-0866','HV0001',4,0)
insert into SOLUONGVE values('MH-202201-0866','HV0002',7,0)
insert into VEMAYBAY values('VE0037','MH-202201-0866','21520001','HV0001',165000,'Da thanh toan','2023-1-29 00:00:00')
insert into VEMAYBAY values('VE0038','MH-202201-0866','21520002','HV0001',165000,'Da thanh toan','2023-1-29 00:00:00')
insert into VEMAYBAY values('VE0039','MH-202201-0866','21520003','HV0002',225000,'Da thanh toan','2023-1-29 00:00:00')
insert into VEMAYBAY values('VE0040','MH-202201-0866','21520004','HV0002',225000,'Da thanh toan','2023-1-29 00:00:00')

insert into CHUYENBAY values('MH-202201-0956','AP0001','AP0008',200000,'2023-1-31 8:30:00',45) 
insert into SOLUONGVE values('MH-202201-0956','HV0001',10,0)
insert into SOLUONGVE values('MH-202201-0956','HV0002',1,0)
insert into VEMAYBAY values('VE0042','MH-202201-0956','21520005','HV0001',220000,'Da thanh toan','2023-1-30 00:00:00')
insert into VEMAYBAY values('VE0043','MH-202201-0956','21520005','HV0002',300000,'Da thanh toan','2023-1-30 00:00:00')

insert into CHUYENBAY values('MH-202202-0957','AP0002','AP0004',100000,'2023-2-15 8:30:00',35) 
insert into SOLUONGVE values('MH-202202-0957','HV0001',10,0)
insert into VEMAYBAY values('VE0044','MH-202202-0957','21520006','HV0002',200000,'Da thanh toan','2023-2-14 00:00:00')
insert into VEMAYBAY values('VE0045','MH-202202-0957','21520007','HV0002',200000,'Da thanh toan','2023-2-14 00:00:00')

insert into CHUYENBAY values('MH-202202-0966','AP0005','AP0007',200000,'2023-2-15 17:30:00',45) 
insert into SOLUONGVE values('MH-202202-0966','HV0002',10,0)
insert into SOLUONGVE values('MH-202202-0966','HV0003',10,0)
insert into VEMAYBAY values('VE0046','MH-202202-0966','21520008','HV0001',220000,'Da thanh toan','2023-2-14 00:00:00')
insert into VEMAYBAY values('VE0047','MH-202202-0966','21520009','HV0002',300000,'Da thanh toan','2023-2-14 00:00:00')
insert into VEMAYBAY values('VE0048','MH-202202-0966','21520010','HV0001',220000,'Da thanh toan','2023-2-14 00:00:00')
insert into VEMAYBAY values('VE0049','MH-202202-0966','21520011','HV0002',300000,'Da thanh toan','2023-2-14 00:00:00')

insert into CHUYENBAY values('MH-202202-0976','AP0005','AP0006',200000,'2023-2-17 20:34:00',55)
insert into SOLUONGVE values('MH-202202-0976','HV0001',8,0)
insert into SOLUONGVE values('MH-202202-0976','HV0002',8,0)
insert into VEMAYBAY values('VE0050','MH-202202-0976','21520013','HV0001',220000,'Da thanh toan','2023-2-16 00:00:00')
insert into VEMAYBAY values('VE0051','MH-202202-0976','21520014','HV0002',300000,'Da thanh toan','2023-2-16 00:00:00')
insert into VEMAYBAY values('VE0052','MH-202202-0976','21520015','HV0001',220000,'Da thanh toan','2023-2-16 00:00:00')
insert into VEMAYBAY values('VE0053','MH-202202-0976','21520001','HV0002',300000,'Da thanh toan','2023-2-16 00:00:00')

insert into CHUYENBAY values('MH-202202-0978','AP0005','AP0006',200000,'2022-2-23 20:34:00',35)
insert into SOLUONGVE values('MH-202202-0978','HV0003',18,0)
insert into VEMAYBAY values('VE0054','MH-202202-0978','21520002','HV0003',400000,'Da thanh toan','2022-2-22 00:00:00')
insert into VEMAYBAY values('VE0055','MH-202202-0978','21520003','HV0003',400000,'Da thanh toan','2022-2-22 00:00:00')

insert into CHUYENBAY values('MH-202203-0981','AP0004','AP0002',200000,'2023-3-15 21:30:00',30)
insert into SOLUONGVE values('MH-202203-0981','HV0001',20,0)
insert into SOLUONGVE values('MH-202203-0981','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0981','HV0003',5,0)
insert into VEMAYBAY values('VE0056','MH-202203-0981','21520004','HV0001',220000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0057','MH-202203-0981','21520005','HV0002',300000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0058','MH-202203-0981','21520006','HV0003',400000,'Da thanh toan','2023-3-14 00:00:00')

insert into CHUYENBAY values('MH-202203-0982','AP0004','AP0002',200000,'2023-3-15 21:30:00',30)
insert into SOLUONGVE values('MH-202203-0982','HV0001',20,0)
insert into SOLUONGVE values('MH-202203-0982','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0982','HV0003',5,0)
insert into VEMAYBAY values('VE0059','MH-202203-0982','21520007','HV0001',220000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0060','MH-202203-0982','21520008','HV0002',300000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0061','MH-202203-0982','21520009','HV0003',400000,'Da thanh toan','2023-3-14 00:00:00')

insert into CHUYENBAY values('MH-202203-0991','AP0004','AP0010',100000,'2023-3-15 20:30:00',30)
insert into SOLUONGVE values('MH-202203-0991','HV0001',20,0)
insert into SOLUONGVE values('MH-202203-0991','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0991','HV0003',5,0)
insert into VEMAYBAY values('VE0062','MH-202203-0991','21520010','HV0001',110000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0063','MH-202203-0991','21520011','HV0002',150000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0064','MH-202203-0991','21520013','HV0001',110000,'Da thanh toan','2023-3-14 00:00:00')
insert into VEMAYBAY values('VE0065','MH-202203-0991','21521619','HV0002',150000,'Da thanh toan','2023-3-14 00:00:00')

insert into CHUYENBAY values('MH-202203-0992','AP0004','AP0010',100000,'2023-3-16 20:30:00',30)
insert into SOLUONGVE values('MH-202203-0992','HV0003',20,0)
insert into VEMAYBAY values('VE0066','MH-202203-0992','21520014','HV0003',200000,'Da thanh toan','2023-3-15 00:00:00')
insert into VEMAYBAY values('VE0067','MH-202203-0992','21520015','HV0003',200000,'Da thanh toan','2023-3-15 00:00:00')

insert into CHUYENBAY values('MH-202203-0993','AP0003','AP0008',110000,'2023-3-25 13:30:00',30)
insert into SOLUONGVE values('MH-202203-0993','HV0001',20,0)
insert into SOLUONGVE values('MH-202203-0993','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0993','HV0003',5,0)
insert into VEMAYBAY values('VE0068','MH-202203-0993','21520001','HV0001',121000,'Da thanh toan','2023-3-24 00:00:00')
insert into VEMAYBAY values('VE0069','MH-202203-0993','21520002','HV0002',165000,'Da thanh toan','2023-3-24 00:00:00')
insert into VEMAYBAY values('VE0070','MH-202203-0993','21520003','HV0003',220000,'Da thanh toan','2023-3-24 00:00:00')
insert into VEMAYBAY values('VE0071','MH-202203-0993','21520004','HV0002',165000,'Da thanh toan','2023-3-24 00:00:00')


insert into CHUYENBAY values('MH-202203-0994','AP0006','AP0008',110000,'2023-3-25 8:30:00',30)
insert into SOLUONGVE values('MH-202203-0994','HV0001',20,0)
insert into VEMAYBAY values('VE0072','MH-202203-0994','21520005','HV0001',121000,'Da thanh toan','2023-3-24 00:00:00')
insert into VEMAYBAY values('VE0073','MH-202203-0994','21520006','HV0001',121000,'Da thanh toan','2023-3-24 00:00:00')


insert into CHUYENBAY values('MH-202203-0998','AP0007','AP0008',110000,'2023-3-29 9:30:00',30)
insert into SOLUONGVE values('MH-202203-0998','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0998','HV0003',5,0)
insert into VEMAYBAY values('VE0075','MH-202203-0998','21520007','HV0002',165000,'Da thanh toan','2023-3-29 00:00:00')
insert into VEMAYBAY values('VE0076','MH-202203-0998','21520008','HV0003',220000,'Da thanh toan','2023-3-29 00:00:00')
insert into VEMAYBAY values('VE0077','MH-202203-0998','21520009','HV0002',165000,'Da thanh toan','2023-3-29 00:00:00')
insert into VEMAYBAY values('VE0078','MH-202203-0998','21520010','HV0003',220000,'Da thanh toan','2023-3-29 00:00:00')

insert into CHUYENBAY values('MH-202203-0999','AP0009','AP0010',110000,'2023-4-25 10:00:00',30)
insert into SOLUONGVE values('MH-202203-0999','HV0002',10,0)
insert into SOLUONGVE values('MH-202203-0999','HV0003',5,0)
insert into VEMAYBAY values('VE0079','MH-202203-0999','21520011','HV0002',165000,'Da thanh toan','2023-4-24 00:00:00')
insert into VEMAYBAY values('VE0080','MH-202203-0999','21520013','HV0003',220000,'Da thanh toan','2023-4-24 00:00:00')

insert into CHUYENBAY values('MH-202204-1000','AP0004','AP0001',110000,'2023-4-25 10:15:00',30)
insert into SOLUONGVE values('MH-202204-1000','HV0003',5,0)
insert into VEMAYBAY values('VE0081','MH-202204-1000','21520014','HV0003',220000,'Da thanh toan','2023-4-24 00:00:00')
insert into VEMAYBAY values('VE0082','MH-202204-1000','21520015','HV0003',220000,'Da thanh toan','2023-4-24 00:00:00')

insert into CHUYENBAY values('MH-202204-1001','AP0004','AP0001',110000,'2023-4-25 10:00:00',30)
insert into SOLUONGVE values('MH-202204-1001','HV0001',15,0)
insert into SOLUONGVE values('MH-202204-1001','HV0002',5,0)
insert into VEMAYBAY values('VE0083','MH-202204-1001','21521619','HV0001',121000,'Da thanh toan','2023-4-24 00:00:00')
insert into VEMAYBAY values('VE0084','MH-202204-1001','21521619','HV0001',121000,'Da thanh toan','2023-4-24 00:00:00')
insert into VEMAYBAY values('VE0085','MH-202204-1001','21521619','HV0001',121000,'Da thanh toan','2023-4-24 00:00:00')
insert into VEMAYBAY values('VE0086','MH-202204-1001','21521619','HV0002',165000,'Da thanh toan','2023-4-24 00:00:00')


insert into CHUYENBAY values('MH-202204-1002','AP0002','AP0008',100000,'2023-4-28 12:30:00',30)
insert into SOLUONGVE values('MH-202204-1002','HV0002',5,0)
insert into VEMAYBAY values('VE0087','MH-202204-1002','21520001','HV0002',150000,'Da thanh toan','2023-4-27 00:00:00')
insert into VEMAYBAY values('VE0088','MH-202204-1002','21520002','HV0002',150000,'Da thanh toan','2023-4-27 00:00:00')
insert into VEMAYBAY values('VE0089','MH-202204-1002','21520003','HV0002',150000,'Da thanh toan','2023-4-27 00:00:00')
insert into VEMAYBAY values('VE0090','MH-202204-1002','21520004','HV0002',150000,'Da thanh toan','2023-4-27 00:00:00')

insert into CHUYENBAY values('MH-202205-1003','AP0001','AP0007',100000,'2023-5-2 13:30:00',30)
insert into SOLUONGVE values('MH-202205-1003','HV0001',15,0)
insert into SOLUONGVE values('MH-202205-1003','HV0002',5,0)
insert into VEMAYBAY values('VE0091','MH-202205-1003','21520002','HV0001',110000,'Da thanh toan','2023-5-2 00:00:00')
insert into VEMAYBAY values('VE0092','MH-202205-1003','21520005','HV0002',150000,'Da thanh toan','2023-5-2 00:00:00')
insert into VEMAYBAY values('VE0093','MH-202205-1003','21520006','HV0001',110000,'Da thanh toan','2023-5-2 00:00:00')
insert into VEMAYBAY values('VE0094','MH-202205-1003','21520007','HV0002',150000,'Da thanh toan','2023-5-2 00:00:00')

insert into CHUYENBAY values('MH-202205-1004','AP0001','AP0002',100000,'2023-5-8 15:30:00',30)
insert into SOLUONGVE values('MH-202205-1004','HV0001',15,0)
insert into SOLUONGVE values('MH-202205-1004','HV0002',5,0)
insert into VEMAYBAY values('VE0095','MH-202205-1004','21520008','HV0001',110000,'Da thanh toan','2023-5-8 00:00:00')
insert into VEMAYBAY values('VE0096','MH-202205-1004','21520009','HV0001',110000,'Da thanh toan','2023-5-8 00:00:00')

insert into CHUYENBAY values('MH-202205-1005','AP0005','AP0003',150000,'2023-5-28 7:30:00',30)
insert into SOLUONGVE values('MH-202205-1005','HV0001',15,0)
insert into SOLUONGVE values('MH-202205-1005','HV0002',5,0)
insert into VEMAYBAY values('VE0097','MH-202205-1005','21520010','HV0001',165000,'Da thanh toan','2023-5-28 00:00:00')
insert into VEMAYBAY values('VE0098','MH-202205-1005','21520011','HV0001',165000,'Da thanh toan','2023-5-28 00:00:00')

insert into CHUYENBAY values('MH-202205-1006','AP0004','AP0007',150000,'2023-5-31 13:30:00',30)
insert into SOLUONGVE values('MH-202205-1006','HV0001',15,0)
insert into SOLUONGVE values('MH-202205-1006','HV0002',5,0)
insert into VEMAYBAY values('VE0099','MH-202205-1006','21520013','HV0001',165000,'Da thanh toan','2023-5-30 00:00:00')
insert into VEMAYBAY values('VE0100','MH-202205-1006','21520013','HV0001',165000,'Chua thanh toan','2023-5-30 09:00:00')


insert into CHUYENBAY values('MH-202205-1007','AP0005','AP0003',150000,'2023-5-31 19:30:00',30)
insert into SOLUONGVE values('MH-202205-1007','HV0001',15,0)
insert into SOLUONGVE values('MH-202205-1007','HV0003',5,0)
insert into VEMAYBAY values('VE0101','MH-202205-1007','21521619','HV0001',165000,'Da thanh toan','2023-5-31 10:00:00')
insert into VEMAYBAY values('VE0102','MH-202205-1007','21521619','HV0001',300000,'Da thanh toan','2023-5-31 15:00:00')

insert into CHUYENBAY values('MH-202206-1008','AP0002','AP0008',150000,'2023-6-1 19:30:00',30)
insert into SOLUONGVE values('MH-202206-1008','HV0001',15,0)
insert into SOLUONGVE values('MH-202206-1008','HV0003',5,0)
insert into VEMAYBAY values('VE0103','MH-202206-1008','21521619','HV0003',300000,'Chua thanh toan','2023-5-31 15:00:00')


insert into CHUYENBAY values('MH-202206-1009','AP0005','AP0003',150000,'2023-6-3 19:30:00',30)
insert into SOLUONGVE values('MH-202206-1009','HV0001',15,0)
insert into SOLUONGVE values('MH-202206-1009','HV0003',5,0)
insert into VEMAYBAY values('VE0104','MH-202206-1009','21520001','HV0001',165000,'Chua thanh toan','2023-6-2 15:00:00')
insert into VEMAYBAY values('VE0105','MH-202206-1009','21520002','HV0003',300000,'Chua thanh toan','2023-6-2 16:00:00')


insert into CHUYENBAY values('MH-202206-1010','AP0004','AP0006',150000,'2023-6-3 19:30:00',30)
insert into SOLUONGVE values('MH-202206-1010','HV0001',15,0)
insert into SOLUONGVE values('MH-202206-1010','HV0003',5,0)
insert into VEMAYBAY values('VE0106','MH-202206-1010','21520003','HV0001',165000,'Chua thanh toan','2023-6-2 15:00:00')
insert into VEMAYBAY values('VE0107','MH-202206-1010','21520004','HV0003',300000,'Chua thanh toan','2023-6-2 16:00:00')


GO
ALTER TRIGGER [dbo].[TRG_VEMAYBAY] ON [dbo].[VEMAYBAY]
FOR INSERT
AS
BEGIN
	DECLARE @COUNT2 INT
	DECLARE @SLGHE INT
	SELECT @SLGHE = COUNT(*) FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	SELECT @COUNT2 = COUNT(*)
	FROM 
	(
		SELECT inserted.MaChuyenBay, inserted.MaHangVe, COUNT(*) AS SoLuongGhe FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	) AS K, SOLUONGVE
	WHERE K.MaChuyenBay = SOLUONGVE.MaChuyenBay AND K.MaHangVe = SOLUONGVE.MaHangVe AND K.SoLuongGhe > SOLUONGVE.TongSoGhe - SOLUONGVE.SoGheDaDat
	IF(@COUNT2>0)
	BEGIN
		PRINT(@SLGHE)
		PRINT(N'ERROR: Không đủ ghế')
		ROLLBACK TRANSACTION
	END

	DECLARE @COUNT3 INT
	SELECT @COUNT3=COUNT(*) FROM inserted,CHUYENBAY,THAMSO WHERE inserted.MaChuyenBay=CHUYENBAY.MaChuyenBay AND inserted.Loaive='Ve Dat Cho' AND (DATEDIFF(HOUR,CURRENT_TIMESTAMP,CHUYENBAY.NgayBay))<THAMSO.ThoiGianDatVe
	IF(@COUNT3 >0)
	BEGIN
		PRINT(N'ERROR: Đã quá thời gian đặt vé!')
		ROLLBACK TRANSACTION
	END
	
	DECLARE @COUNT4 INT
	SELECT @COUNT4=COUNT(*) FROM inserted,CHUYENBAY,THAMSO WHERE inserted.MaChuyenBay=CHUYENBAY.MaChuyenBay AND inserted.Loaive='Ve Mua' AND (DATEDIFF(HOUR,CURRENT_TIMESTAMP,CHUYENBAY.NgayBay))<THAMSO.ThoiGianHuyVe
	IF(@COUNT4 >0)
	BEGIN
		PRINT(N'ERROR: Đã quá thời gian mua vé!')
		ROLLBACK TRANSACTION
	END

	UPDATE SOLUONGVE SET SoGheDaDat = SoGheDaDat + 1
	WHERE SOLUONGVE.MaChuyenBay IN (SELECT MaChuyenBay FROM inserted )
	AND SOLUONGVE.MaHangVe IN (SELECT MaHangVe FROM inserted WHERE inserted.MaChuyenBay = SOLUONGVE.MaChuyenBay)

END

