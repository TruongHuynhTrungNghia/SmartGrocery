alter table `base_product` drop foreign key `FK_base_product_transaction_Transaction_Id`;
alter table `base_product` drop index `IX_Transaction_Id`;
create table `ProductSnapshots` (`Id` CHAR(36) BINARY default ''  not null ,`ProductId` CHAR(36) BINARY default ''  not null ,`TransactionId` CHAR(36) BINARY default ''  not null ,`NumberOfSoldProduct` int not null ,`RemainingProduct` int not null ,`Status` longtext,primary key ( `Id`) ) engine=InnoDb auto_increment=0;
CREATE index  `IX_ProductId` on `ProductSnapshots` (`ProductId` DESC) using HASH;
CREATE index  `IX_TransactionId` on `ProductSnapshots` (`TransactionId` DESC) using HASH;
alter table `base_product` drop column `Transaction_Id`;
alter table `ProductSnapshots` add constraint `FK_ProductSnapshots_base_product_ProductId`  foreign key (`ProductId`) references `base_product` ( `Id`) ;
alter table `ProductSnapshots` add constraint `FK_ProductSnapshots_transaction_TransactionId`  foreign key (`TransactionId`) references `transaction` ( `Id`) ;
INSERT INTO `__MigrationHistory`(
`MigrationId`, 
`ContextKey`, 
`Model`, 
`ProductVersion`) VALUES (
'202004021624198_ImplementedProductSnapshot', 
'SmartGrocery.UseCase.DAL.Migrations.Configuration', 
0x1F8B0800000000000400ED5D5B6FE3BA117E2FD0FF20E8B1D8B5932CB6DD06F639F03AC922E8E6D2387BD0B780911847A86EA5A82046D15F761ECE4FEA5F28A92BAF1229CB71721A2C102812F971389C190E8733D9FFFEFADBECE7E728749E20CA82249EBB879303D781B197F841BC9EBB397EF8F8C5FDF9A73FFE6176EA47CFCE2F75BB4FB41DE9196773F711E3F4783ACDBC4718816C12051E4AB2E4014FBC249A023F991E1D1CFC757A78388504C225588E33BBC9631C44B0F885FCBA4C620FA63807E145E2C330ABDE932FAB02D5B90411CC52E0C1B9BB8A00C2DF50E241B499FCC8E012647072B2F8EE3A8B3000849E150C1F5C07C471820126D41E93462B8C9278BD4AC90B10DE6E5248DA3D803083D52C8EDBE6A6133A38A2139AB61D6B282FCF701259021E7EAA383415BB0FE2B3DB7090F0F094F01A6FE8AC0B3ECEDDAF8465D728F1730FBB8E38DEF13244B4ADC0E962612655AF0983F0C191DB7D6824850814FDF7C159E621CE119CC730C7089016D7F97D18787F839BDBE49F309EC77918B25413BAC937EE057945C64C21C29B1BF850CDE5DC779D29DF6F2A766CBA317DCA297ECB03F27C49C606F7216C6482E1C60A27087E8331440043FF1A600C1159D2CB2486D2C0C230F4673D101140A251AE73019EBFC3788D1FE7EED1E73FBBCE59F00CFDFA4D35FA8F38200A483A6194430575DDA35EA3C06B863D815E1081D075AE1179AA74FC8BEBAC3C40018FACD1FF9E83429AEA01CE63FCC91EE5F4390DD0E68470B421943CDF0691FD742F409C3F008FC816E1EF2888955C5FE6D13D441DEB7778F46594F5BB044FC1BA10363521AB18A4D9634254F5068645BBEC31484B3B3711DADC356A7D8692E826096598BAC9DD2D406B485ADE26DDED56498E3C81ECD9B4352A9DA6469AC3207323A0BC9B9C1ED9ED1DAD1BE5168138233A4588D912A954A2AB875512FA8D686E63396EC8DE17C4441547415B118EE759878E9347231DB754E9B155B956D13E55AE55DE945C46108C48E6DAEBC9669AF592CEB655916F6C8938DAACAC10D3937D7EB74206F6A377171DA861C2888B2821278A1186B134204B042977BE6EF636F4026FEDF07C0719FE91FAFB9A0833FC621F4B589802886C773AADD1AC019516B3FEC8DAB58CB595CA0692AFA66EA5F2D4C676300798F83E4753B51D0C32F12DE7ADEC7BDDAD7978B7EC5DBADA73ACDD919A9E05685F43CB16E2E5C6A646FDEAE16B80F0E3D6767EB186DBB9CBD74910E34CC0E871056036864D1FE0B06663DA5FD14BEDB6D2C34ECA10454196D9BBA794A249DBFBDD78E986E9B11E63C573440D86998782B4DC30B5837FFA7C30AA2250A1506B402B29775523661717BEC9A733B1C156224F2186083BFDF12EE66F51CC478FA734F23882B04B7EAA4E1B4C847D9165891714F4A8BDED26BEC34FF534F61DC3604FC96485934E584EC43E4889A013CAE6EE9F2486F68FD230A31D85BB36E24738944620AA02118CE97DDB922C09513EE23BC87A15C45E9082D08C18A1BBA162D2356A0612BF9CC014C63EA1D38CE926143031589990663CC176F4B16B366504AA5BCED42E8E6EFD7BFC9D76F5DBC395B8F4E224AFE21318420C9D8557DE7F2E41E6015F566BA23ABE1D5D0AA9E40EA47D723F482A3B59F40232D9C90A93F1D903CC5E44B22B88606A989441E39D984065ECC248E07664061504EDC1142A16C0840AE132693FF227BA015A51D03AC08CA43107350B21D37A1B2D72E90F77626A275D7A21845198B009A2FA828BF191E937F8ACBA82A55934A5CF9D550E98380B8ABD8258CE1F211E55EBFFA8FC0489213C9420642A3849B37B20F993BD04C7A96F0F546D385538ED7ED83745D6479567C748530F50E5C14A10A5D8089D1931D172BBBD62645AF7DC468AEA6BEEAF36D351ADBA6416CC3D540697174CD1DBE05962C02E4DB048669681CB65E17431136204B08343DDBE1203C7CF636BFE74DE0DF48B94D60BB0F603C6122DD5CEBF43F649875305CF3A772EB3BD8BE50E6B8BBA18A3DBAD18AC8A666336D427E5668F6ABECDA6659A67F56236D5E483CE2E409A06F19AC90FADDE38AB323974F971659F2F199518532F53A44D36D43623E104813514BE92A109A5C525C509C0E01ED0A0C6D28FA466AA1D5963F6EB11159BAEBC72F59E5077A2CFB20FC026CC72FBB4ECC955406764B211750A8B8894DAD6CABD1D9AB90B42801421B06512E651AC7751F5BDCBC816DBBF7C638E502547B210D52B738C3605928569DF9A23B169902C16FBDE1C4D9109C9822A3EDBF08DCB89E4F9C77D923167534194A4138024BD529880D708237D9136825174467446EDF5A6176137BAC344A514AB6787251CE958BC9ED39E1E539929C8A9BAAA8139BE9C38C882CB5FCD91EB244216AF7EF76AB4A1C7311DA0091DDE9B811674F6DE8D0628B2D23492ABB7653AEC3AFF8C05ACDF99A330D9642C10F3DA1A6B8195580B2BBA840431164FF8340853A451F864316726CEC94DBA23FEB9378D6C8F76A3A863138EB0D7457DD7DD28629B4424AEBAAD3BC72405B150CCEB5D0B8F0E8DCBDB61E1B80F1606662DCCB17861B1FD57A93BDCDE5FBDB330A145360F67358B37AF46A9D8F3ED38FE5E1B9E1BE0EA75747EAD2724EE1E9F135CF6C3AB59705D5064C052176154FB4556777B5F5EBBE5E5A3463AA5AEE364568A5B77D26BA76A0969304C9B6CC207D86466192D3D03A7BE18632818409CF6A2D088388A61499618FA93D7598A008A4D1A296B228142C46F5645DFFACBC4A5705CD98456C2264F814F4371179BD5BFC209FD3E291E976100A9C35EB7B80071F040F6B73253CCFD3CF98B5063FE7AEABDA759E6878AE8A5A6E89B5FB71748785B0F2A1E046D225BFC0490F708905C41BD4D79B4BFEBF2E880CAD3D6C5D1F428848B8C6F86CE83F14AA547C21722811D0B57E42076A20F2E307E1BA2CD84DD5814E966FE3CF6E1F3DCFD77D1EDD839FFC75DD3F383738588913A760E9CFF8C53E16B4C03D77B2B3A3AEA8387A88EAE3A7808165F1B1C2644728BEC8916464E551D548CFA3624565BD369C61919902FD954A10CABC0DC16A9AD411CC92C2ACB2B0753A9AC961C3E67A9B4C9D808B45D2D2C807D29DFDBD00EB12E6EF08248656E232EED60284511DA48BAC194A40DB1D17C419A80606284D8F2B4914DBC2EF8F36A65B8D7DDEEF7DA14A223978528B18BFAA751F82E1FE6FFEF39BE33C91ECAEBBED087DC43138FE85F2261285B6F9BEDBD95A7DB4CC09282AADF087BECF615495C62EFD0C2206DF6DD76E9FC8312EDBB33A04C6C84B34546FDEFB89A689F95435C16EA5E0A855E4296BAD32FE4E1DE640DD080A57CDD06C776D5F660705E63BD8EF2A64599AE6C58A9B323D32378466A5A0CAA7C868A57E76DF5F8D2D5798136C8F1DB8B70A96ECA9845959652B580BF5381325ED89717A58E1BCAD185485D5C2767FC8BCBA82D9BEBAB9A2B2F2BE7AE7F9F90452F5D7D3AC25D3AACAECEA4AC4E35A694446D517CD7577BA71A0F0F28CEEBACCD530DE2D957EEF514EEA94649ED0AFBB4757D2A6CA4AAF8D353AFC1171B74CF22FB18C41F91B2E6E7E56B0D15AA2415A558147E755590BD853A4265CD602703147A2AE565BF9102C10153D9A32CE85C5B83DA3F4DBE91C60F56E7219A15FDB10643952BB50336D40586066C506736491E9B98A3F7D253B7A87794B39A88B3C2FC1719C45FCA82750B41FFC38C187A9C9BD2B4398F1F92DA5D1228AA9B88892110039FF8300B84039A20724DF3E9C894E91F6FFA05843969721ADD43FF3CBECA719A63326518DD875CBE3FF5BABAC62F8A3A799A675745943A1B630A84CC80DE495DC55FF320F41BBACF14F1510D0475E7AABFB845D712D3BFBCB5DE3448F29FDED20155EC6BBCD05B18A52101CBAEE21578824368FB91C1EF700DBC4D9D9BA607E95F089EEDB39300AC1188B20AA3ED4F7E2532EC47CF3FFD0F3B4F376129660000, 
'6.2.0-61023');