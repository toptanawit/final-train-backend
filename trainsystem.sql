-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Mar 26, 2024 at 12:49 AM
-- Server version: 10.4.17-MariaDB
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `trainsystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `density_parkinglot_new`
--

CREATE TABLE `density_parkinglot_new` (
  `dp_id` int(11) NOT NULL,
  `user_id` varchar(100) NOT NULL,
  `parking_id` varchar(10) NOT NULL,
  `vehicle` varchar(20) NOT NULL,
  `status` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `density_station_new`
--

CREATE TABLE `density_station_new` (
  `ds_id` int(11) NOT NULL,
  `user_id` varchar(100) NOT NULL,
  `station_id` varchar(10) NOT NULL,
  `status` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `density_station_new`
--

INSERT INTO `density_station_new` (`ds_id`, `user_id`, `station_id`, `status`) VALUES
(1, '1234', 'E4', 1);

-- --------------------------------------------------------

--
-- Table structure for table `stations`
--

CREATE TABLE `stations` (
  `station_id` varchar(10) NOT NULL,
  `station_name` varchar(100) DEFAULT NULL,
  `station_line` varchar(5) DEFAULT NULL,
  `station_linecolor` varchar(10) DEFAULT NULL,
  `is_extended` tinyint(1) NOT NULL,
  `latitude` double NOT NULL,
  `longitude` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stations`
--

INSERT INTO `stations` (`station_id`, `station_name`, `station_line`, `station_linecolor`, `is_extended`, `latitude`, `longitude`) VALUES
('A1', 'Suvarnabhumi ARL', 'arl', 'red', 0, 13.703119561182797, 100.75187358910955),
('A2', 'Lat Krabang ARL', 'arl', 'red', 0, 13.734276250159681, 100.74759724392246),
('A3', 'Ban Thap Chang ARL', 'arl', 'red', 0, 13.727890000318158, 100.68836206186987),
('A4', 'Hua Mak ARL', 'arl', 'red', 0, 13.746004409350032, 100.64560053511678),
('A5', 'Ramkhamhaeng ARL', 'arl', 'red', 0, 13.749561204985838, 100.60109728071038),
('A6', 'Makkasan ARL', 'arl', 'red', 0, 13.75835007181401, 100.56076481053812),
('A7', 'Ratchaprarop ARL', 'arl', 'red', 0, 13.761253848376082, 100.54256523334259),
('A8', 'Phaya Thai ARL', 'arl', 'red', 0, 13.763686630249191, 100.53387434233079),
('BL01', 'Tha Phra MRT', 'mrt', 'blue', 0, 13.731494442817219, 100.4737739810618),
('BL02', 'Charan 13 MRT', 'mrt', 'blue', 0, 13.744401607301901, 100.4711395266396),
('BL03', 'Fai Chai MRT', 'mrt', 'blue', 0, 13.754260567319378, 100.46907428137094),
('BL04', 'Bang Khun Non MRT', 'mrt', 'blue', 0, 13.766710663842208, 100.4740320496383),
('BL05', 'Bang Yi Khan MRT', 'mrt', 'blue', 0, 13.775785491167852, 100.485663927079),
('BL06', 'Sirindhorn MRT', 'mrt', 'blue', 0, 13.783803275981889, 100.49507269533747),
('BL07', 'Bang Phlat MRT', 'mrt', 'blue', 0, 13.792203376416715, 100.50458398180554),
('BL08', 'Bang O MRT', 'mrt', 'blue', 0, 13.79690559814871, 100.51213666369202),
('BL09', 'Bang Pho MRT', 'mrt', 'blue', 0, 13.806249477153719, 100.52047498197255),
('BL10', 'Tao Poon MRT', 'mrt', 'blue', 0, 13.806884887217254, 100.53045198196823),
('BL11', 'Bang Sue MRT', 'mrt', 'blue', 0, 13.802877876975662, 100.53967692738516),
('BL12', 'Kamphaeng Phet MRT', 'mrt', 'blue', 0, 13.796887728047047, 100.54793332732578),
('BL13', 'Chatuchak Park MRT', 'mrt', 'blue', 0, 13.804379922895247, 100.55346668192652),
('BL14', 'Phahon Yothin MRT', 'mrt', 'blue', 0, 13.813016555265865, 100.56155281070602),
('BL15', 'Lat Phrao MRT', 'mrt', 'blue', 0, 13.805605272900033, 100.57475547287423),
('BL16', 'Ratchadaphisek MRT', 'mrt', 'blue', 0, 13.800278043165648, 100.57391105915534),
('BL17', 'Sutthisan MRT', 'mrt', 'blue', 0, 13.788136690085652, 100.57452332722174),
('BL18', 'Huai Khwang MRT', 'mrt', 'blue', 0, 13.778281975698498, 100.57300145891378),
('BL19', 'Thailand Cultural Centre MRT', 'mrt', 'blue', 0, 13.765702913863889, 100.5730136860319),
('BL20', 'Phra Ram 9 MRT', 'mrt', 'blue', 0, 13.757743095140178, 100.56440075865818),
('BL21', 'Phetchaburi MRT', 'mrt', 'blue', 0, 13.747158126501581, 100.56519404037115),
('BL22', 'Sukhumvit MRT', 'mrt', 'blue', 0, 13.737349470330004, 100.5617881266193),
('BL23', 'Queen Sirikit National Convention Centre MRT', 'mrt', 'blue', 0, 13.723215517771417, 100.56108817188499),
('BL24', 'Khlong Toei MRT', 'mrt', 'blue', 0, 13.721450924978273, 100.55679608551162),
('BL25', 'Lumphini MRT', 'mrt', 'blue', 0, 13.72433359548904, 100.54366989011113),
('BL26', 'Si Lom MRT', 'mrt', 'blue', 0, 13.72740679928504, 100.53648390378315),
('BL27', 'Sam Yan MRT', 'mrt', 'blue', 0, 13.733327898225664, 100.5298511038217),
('BL28', 'Hua Lamphong MRT', 'mrt', 'blue', 0, 13.735994160776018, 100.51818117206385),
('BL29', 'Wat Mangkon MRT', 'mrt', 'blue', 0, 13.741822673875278, 100.5100464039341),
('BL30', 'Sam Yot MRT', 'mrt', 'blue', 0, 13.746956374126938, 100.50168508126856),
('BL31', 'Sanam Chai MRT', 'mrt', 'blue', 0, 13.745040441326811, 100.49489422668276),
('BL32', 'Itsaraphap MRT', 'mrt', 'blue', 0, 13.737211969736327, 100.48638357207038),
('BL33', 'Bang Phai MRT', 'mrt', 'blue', 0, 13.723863431952067, 100.4644832582767),
('BL34', 'Bang Wa MRT', 'mrt', 'blue', 0, 13.72058972511087, 100.45661208094856),
('BL35', 'Phetkasem 48 MRT', 'mrt', 'blue', 0, 13.714615005226165, 100.44591732634639),
('BL36', 'Phasi Charoen MRT', 'mrt', 'blue', 0, 13.711785860075315, 100.43660823994865),
('BL37', 'Bang Khae MRT', 'mrt', 'blue', 0, 13.711302933800036, 100.423317871756),
('BL38', 'Lak Song MRT', 'mrt', 'blue', 0, 13.71083020192932, 100.40940976652891),
('CEN', 'Siam BTS', 'bts', 'lightgreen', 0, 13.74608775379938, 100.53401578125037),
('E1', 'Chit Lom BTS', 'bts', 'lightgreen', 0, 13.74323128799462, 100.54507304031672),
('E10', 'Bang Chak BTS', 'bts', 'lightgreen', 0, 13.694583804659615, 100.60536650339921),
('E11', 'Punnawithi BTS', 'bts', 'lightgreen', 0, 13.689765511196482, 100.6106807169425),
('E12', 'Udomsuk BTS', 'bts', 'lightgreen', 0, 13.679880240321948, 100.60940474326465),
('E13', 'Bang Na BTS', 'bts', 'lightgreen', 0, 13.670955254373157, 100.60465380306147),
('E14', 'Bearing BTS', 'bts', 'lightgreen', 0, 13.660356548889503, 100.60218242570622),
('E15', 'Samrong BTS', 'bts', 'lightgreen', 1, 13.647680128516765, 100.59799123917303),
('E16', 'Pu Chao BTS', 'bts', 'lightgreen', 1, 13.639289266833874, 100.59057091179126),
('E17', 'Chang Erawan BTS', 'bts', 'lightgreen', 1, 13.622463064502767, 100.58876791160928),
('E18', 'Royal Thai Naval Academy BTS', 'bts', 'lightgreen', 1, 13.606992325810355, 100.59617589326587),
('E19', 'Pak Nam BTS', 'bts', 'lightgreen', 1, 13.603610829696395, 100.59670207955925),
('E2', 'Phloen Chit BTS', 'bts', 'lightgreen', 0, 13.743171167713719, 100.55139396303053),
('E20', 'Srinagarindra BTS', 'bts', 'lightgreen', 1, 13.593246567308775, 100.60784367945735),
('E21', 'Phraek Sa BTS', 'bts', 'lightgreen', 1, 13.585460937891156, 100.60902647025586),
('E22', 'Sai Luat BTS', 'bts', 'lightgreen', 1, 13.576995338214633, 100.60668689290263),
('E23', 'Kheha BTS', 'bts', 'lightgreen', 1, 13.568221149343923, 100.60489802007211),
('E3', 'Nana BTS', 'bts', 'lightgreen', 0, 13.740329173802378, 100.55369159028467),
('E4', 'Asok BTS', 'bts', 'lightgreen', 0, 13.736665573624121, 100.55926601296763),
('E5', 'Phrom Phong BTS', 'bts', 'lightgreen', 0, 13.731677916737103, 100.57128031742991),
('E6', 'Thong Lo BTS', 'bts', 'lightgreen', 0, 13.724064573017483, 100.58196603098737),
('E7', 'Ekkamai BTS', 'bts', 'lightgreen', 0, 13.718654116895493, 100.58685291729954),
('E8', 'Phra Khanong BTS', 'bts', 'lightgreen', 0, 13.714707638796671, 100.58801879908009),
('E9', 'On Nut BTS', 'bts', 'lightgreen', 0, 13.703728129948757, 100.60095080350322),
('N1', 'Ratchathewi BTS', 'bts', 'lightgreen', 0, 13.751486892894324, 100.5319475267789),
('N10', 'Phahon Yothin 24 BTS', 'bts', 'lightgreen', 1, 13.823559999549746, 100.56539733672993),
('N11', 'Ratchayothin BTS', 'bts', 'lightgreen', 1, 13.830185217953575, 100.57084727315505),
('N12', 'Sena Nikhom BTS', 'bts', 'lightgreen', 1, 13.837566817479395, 100.57093072324525),
('N13', 'Kasetsart University BTS', 'bts', 'lightgreen', 1, 13.842830882403867, 100.57650875967408),
('N14', 'Royal Forest Department BTS', 'bts', 'lightgreen', 1, 13.852519511855522, 100.58024839159475),
('N15', 'Bang Bua BTS', 'bts', 'lightgreen', 1, 13.855846279855253, 100.5856202280164),
('N16', '11th Infantry Regiment BTS', 'bts', 'lightgreen', 1, 13.865221258886157, 100.59316787360896),
('N17', 'Wat Phra Sri Mahathat BTS', 'bts', 'lightgreen', 1, 13.875483785497229, 100.59708092824702),
('N18', 'Phahon Yothin 59 BTS', 'bts', 'lightgreen', 1, 13.883118763509941, 100.6004028828777),
('N19', 'Sai Yut BTS', 'bts', 'lightgreen', 1, 13.889546521901593, 100.6029240147716),
('N2', 'Phaya Thai BTS', 'bts', 'lightgreen', 0, 13.757884794541479, 100.53146834502816),
('N20', 'Saphan Mai BTS', 'bts', 'lightgreen', 1, 13.898440918575758, 100.60631792396468),
('N21', 'Bhumibol Adulyadej Hospital BTS', 'bts', 'lightgreen', 1, 13.909398364801326, 100.6185052741257),
('N22', 'Royal Thai Air Force Museum BTS', 'bts', 'lightgreen', 1, 13.921349033744823, 100.62033401512461),
('N23', 'Yaek Kor Por Aor BTS', 'bts', 'lightgreen', 1, 13.925554549296667, 100.62477533793738),
('N24', 'Khu Khot BTS', 'bts', 'lightgreen', 1, 13.932153384313903, 100.64682062892996),
('N3', 'Victory Monument BTS', 'bts', 'lightgreen', 0, 13.764469662708665, 100.53729122690513),
('N4', 'Sanam Pao BTS', 'bts', 'lightgreen', 0, 13.772095723304542, 100.54419394065506),
('N5', 'Ari BTS', 'bts', 'lightgreen', 0, 13.780825439075091, 100.54287689074913),
('N7', 'Saphan Kwai BTS', 'bts', 'lightgreen', 0, 13.793345070989371, 100.54948724875098),
('N8', 'Mo Chit BTS', 'bts', 'lightgreen', 0, 13.803046294171311, 100.55243671374747),
('N9', 'Ha Yaek Lad Phrao BTS', 'bts', 'lightgreen', 1, 13.815463208719454, 100.56097733664144),
('PP01', 'Khlong Bang Phai MRT', 'mrt', 'purple', 0, 13.89221340193892, 100.40841020572884),
('PP02', 'Talat Bang Yai MRT', 'mrt', 'purple', 0, 13.879871389423247, 100.40987272831546),
('PP03', 'Sam Yaek Bang Yai MRT', 'mrt', 'purple', 0, 13.875248891167354, 100.42044677369296),
('PP04', 'Bang Phlu MRT', 'mrt', 'purple', 0, 13.875332476690982, 100.43176716916881),
('PP05', 'Bang Rak Yai MRT', 'mrt', 'purple', 0, 13.877204085659987, 100.44777918734806),
('PP06', 'Bang Rak Noi - Tha It MRT', 'mrt', 'purple', 0, 13.874427875339336, 100.45617400551606),
('PP07', 'Sai Ma MRT', 'mrt', 'purple', 0, 13.871683523582483, 100.46629408273697),
('PP08', 'Phra Nangklao Bridge MRT', 'mrt', 'purple', 0, 13.871416625100903, 100.48329780999728),
('PP09', 'Yaek Nonthaburi 1 MRT', 'mrt', 'purple', 0, 13.868046397095506, 100.49280551450255),
('PP10', 'Bang Kraso MRT', 'mrt', 'purple', 0, 13.860891930600086, 100.50638871899001),
('PP11', 'Nonthaburi Civic Center MRT', 'mrt', 'purple', 0, 13.859134998883464, 100.51239025988856),
('PP12', 'Ministry of Public Health MRT', 'mrt', 'purple', 0, 13.847314247342114, 100.51539925065285),
('PP13', 'Yaek Tiwanon MRT', 'mrt', 'purple', 0, 13.84003499026566, 100.51729156417781),
('PP14', 'Wong Sawang MRT', 'mrt', 'purple', 0, 13.829766878415423, 100.52599698225262),
('PP15', 'Bang Son MRT', 'mrt', 'purple', 0, 13.819395593363906, 100.53279792758569),
('PP16', 'Tao Poon MRT', 'mrt', 'purple', 0, 13.805217868028816, 100.53113862742092),
('S1', 'Ratchadamri BTS', 'bts', 'darkgreen', 0, 13.744399553717368, 100.53940911236552),
('S10', 'Talad Plu BTS', 'bts', 'darkgreen', 0, 13.726719924331608, 100.47675991206717),
('S11', 'Wuttakat BTS', 'bts', 'darkgreen', 0, 13.71937144694258, 100.46945155758927),
('S12', 'Bang Wa BTS', 'bts', 'darkgreen', 0, 13.725738364268715, 100.45950322598381),
('S2', 'Sala Daeng BTS', 'bts', 'darkgreen', 0, 13.734186606766997, 100.53466633500454),
('S3', 'Chong Nonsi BTS', 'bts', 'darkgreen', 0, 13.730092650295491, 100.52861456664476),
('S4', 'Saint Louis BTS', 'bts', 'darkgreen', 0, 13.720162447443604, 100.52738343551843),
('S5', 'Surasak BTS', 'bts', 'darkgreen', 0, 13.724903737309301, 100.52210215766259),
('S6', 'Saphan Taksin BTS', 'bts', 'darkgreen', 0, 13.725332047813877, 100.51433717867866),
('S7', 'Krung Thon Buri BTS', 'bts', 'darkgreen', 0, 13.72721692988602, 100.50395980321768),
('S8', 'Wongwian Yai BTS', 'bts', 'darkgreen', 0, 13.726719924331608, 100.49547620230776),
('S9', 'Pho Nimit BTS', 'bts', 'darkgreen', 0, 13.72559967463935, 100.48594451212725),
('W1', 'National Stadium BTS', 'bts', 'darkgreen', 0, 13.754784492353917, 100.52946873521685);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `density_parkinglot_new`
--
ALTER TABLE `density_parkinglot_new`
  ADD PRIMARY KEY (`dp_id`);

--
-- Indexes for table `density_station_new`
--
ALTER TABLE `density_station_new`
  ADD PRIMARY KEY (`ds_id`);

--
-- Indexes for table `stations`
--
ALTER TABLE `stations`
  ADD PRIMARY KEY (`station_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `density_parkinglot_new`
--
ALTER TABLE `density_parkinglot_new`
  MODIFY `dp_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `density_station_new`
--
ALTER TABLE `density_station_new`
  MODIFY `ds_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
