﻿namespace PixelWorldsServer.Protocol;

public static class CountryCodes
{
    public static readonly Dictionary<short, string> Map = new()
    {
        { 4, "Afghanistan AFG" },
        { 248, "Åland Islands ALA" },
        { 8, "Albania ALB" },
        { 12, "Algeria DZA" },
        { 16, "American Samoa ASM" },
        { 20, "Andorra AND" },
        { 24, "Angola AGO" },
        { 660, "Anguilla AIA" },
        { 10, "Antarctica ATA" },
        { 28, "Antigua and Barbuda ATG" },
        { 32, "Argentina ARG" },
        { 51, "Armenia ARM" },
        { 533, "Aruba ABW" },
        { 36, "Australia AUS" },
        { 40, "Austria AUT" },
        { 31, "Azerbaijan AZE" },
        { 44, "Bahamas BHS" },
        { 48, "Bahrain BHR" },
        { 50, "Bangladesh BGD" },
        { 52, "Barbados BRB" },
        { 112, "Belarus BLR" },
        { 56, "Belgium BEL" },
        { 84, "Belize BLZ" },
        { 204, "Benin BEN" },
        { 60, "Bermuda BMU" },
        { 64, "Bhutan BTN" },
        { 68, "Bolivia (Plurinational State of) BOL" },
        { 535, "Bonaire, Sint Eustatius and Saba BES" },
        { 70, "Bosnia and Herzegovina BIH" },
        { 72, "Botswana BWA" },
        { 74, "Bouvet Island BVT" },
        { 76, "Brazil BRA" },
        { 86, "British Indian Ocean Territory IOT" },
        { 96, "Brunei Darussalam BRN" },
        { 100, "Bulgaria BGR" },
        { 854, "Burkina Faso BFA" },
        { 108, "Burundi BDI" },
        { 116, "Cambodia KHM" },
        { 120, "Cameroon CMR" },
        { 124, "Canada CAN" },
        { 132, "Cabo Verde CPV" },
        { 136, "Cayman Islands CYM" },
        { 140, "Central African Republic CAF" },
        { 148, "Chad TCD" },
        { 152, "Chile CHL" },
        { 156, "China CHN" },
        { 162, "Christmas Island CXR" },
        { 166, "Cocos (Keeling) Islands CCK" },
        { 170, "Colombia COL" },
        { 174, "Comoros COM" },
        { 178, "Congo COG" },
        { 180, "Congo (Democratic Republic of the) COD" },
        { 184, "Cook Islands COK" },
        { 188, "Costa Rica CRI" },
        { 384, "Côte d'Ivoire CIV" },
        { 191, "Croatia HRV" },
        { 192, "Cuba CUB" },
        { 531, "Curaçao CUW" },
        { 196, "Cyprus CYP" },
        { 203, "Czech Republic CZE" },
        { 208, "Denmark DNK" },
        { 262, "Djibouti DJI" },
        { 212, "Dominica DMA" },
        { 214, "Dominican Republic DOM" },
        { 218, "Ecuador ECU" },
        { 818, "Egypt EGY" },
        { 222, "El Salvador SLV" },
        { 226, "Equatorial Guinea GNQ" },
        { 232, "Eritrea ERI" },
        { 233, "Estonia EST" },
        { 231, "Ethiopia ETH" },
        { 238, "Falkland Islands (Malvinas) FLK" },
        { 234, "Faroe Islands FRO" },
        { 242, "Fiji FJI" },
        { 246, "Finland FIN" },
        { 250, "France FRA" },
        { 254, "French Guiana GUF" },
        { 258, "French Polynesia PYF" },
        { 260, "French Southern Territories ATF" },
        { 266, "Gabon GAB" },
        { 270, "Gambia GMB" },
        { 268, "Georgia GEO" },
        { 276, "Germany DEU" },
        { 288, "Ghana GHA" },
        { 292, "Gibraltar GIB" },
        { 300, "Greece GRC" },
        { 304, "Greenland GRL" },
        { 308, "Grenada GRD" },
        { 312, "Guadeloupe GLP" },
        { 316, "Guam GUM" },
        { 320, "Guatemala GTM" },
        { 831, "Guernsey GGY" },
        { 324, "Guinea GIN" },
        { 624, "Guinea-Bissau GNB" },
        { 328, "Guyana GUY" },
        { 332, "Haiti HTI" },
        { 334, "Heard Island and McDonald Islands HMD" },
        { 336, "Holy See VAT" },
        { 340, "Honduras HND" },
        { 344, "Hong Kong HKG" },
        { 348, "Hungary HUN" },
        { 352, "Iceland ISL" },
        { 356, "India IND" },
        { 360, "Indonesia IDN" }, // indog woof woof
        { 364, "Iran (Islamic Republic of) IRN" },
        { 368, "Iraq IRQ" },
        { 372, "Ireland IRL" },
        { 833, "Isle of Man IMN" },
        { 376, "Israel ISR" },
        { 380, "Italy ITA" },
        { 388, "Jamaica JAM" },
        { 392, "Japan JPN" },
        { 832, "Jersey JEY" },
        { 400, "Jordan JOR" },
        { 398, "Kazakhstan KAZ" },
        { 404, "Kenya KEN" },
        { 296, "Kiribati KIR" },
        { 408, "Korea (Democratic People's Republic of) PRK" },
        { 410, "Korea (Republic of) KOR" },
        { 414, "Kuwait KWT" },
        { 417, "Kyrgyzstan KGZ" },
        { 418, "Lao People's Democratic Republic LAO" },
        { 428, "Latvia LVA" },
        { 422, "Lebanon LBN" },
        { 426, "Lesotho LSO" },
        { 430, "Liberia LBR" },
        { 434, "Libya LBY" },
        { 438, "Liechtenstein LIE" },
        { 440, "Lithuania LTU" },
        { 442, "Luxembourg LUX" },
        { 446, "Macao MAC" },
        { 807, "Macedonia (the former Yugoslav Republic of) MKD" },
        { 450, "Madagascar MDG" },
        { 454, "Malawi MWI" },
        { 458, "Malaysia MYS" },
        { 462, "Maldives MDV" },
        { 466, "Mali MLI" },
        { 470, "Malta MLT" },
        { 584, "Marshall Islands MHL" },
        { 474, "Martinique MTQ" },
        { 478, "Mauritania MRT" },
        { 480, "Mauritius MUS" },
        { 175, "Mayotte MYT" },
        { 484, "Mexico MEX" },
        { 583, "Micronesia (Federated States of) FSM" },
        { 498, "Moldova (Republic of) MDA" },
        { 492, "Monaco MCO" },
        { 496, "Mongolia MNG" },
        { 499, "Montenegro MNE" },
        { 500, "Montserrat MSR" },
        { 504, "Morocco MAR" },
        { 508, "Mozambique MOZ" },
        { 104, "Myanmar MMR" },
        { 516, "Namibia NAM" },
        { 520, "Nauru NRU" },
        { 524, "Nepal NPL" },
        { 999, "Neutral" },
        { 528, "Netherlands NLD" },
        { 540, "New Caledonia NCL" },
        { 554, "New Zealand NZL" },
        { 558, "Nicaragua NIC" },
        { 562, "Niger NER" },
        { 566, "Nigeria NGA" },
        { 570, "Niue NIU" },
        { 574, "Norfolk Island NFK" },
        { 580, "Northern Mariana Islands MNP" },
        { 578, "Norway NOR" },
        { 512, "Oman OMN" },
        { 586, "Pakistan PAK" },
        { 585, "Palau PLW" },
        { 275, "Palestine, State of PSE" },
        { 591, "Panama PAN" },
        { 598, "Papua New Guinea PNG" },
        { 600, "Paraguay PRY" },
        { 604, "Peru PER" },
        { 608, "Philippines PHL" },
        { 612, "Pitcairn PCN" },
        { 616, "Poland POL" },
        { 620, "Portugal PRT" },
        { 630, "Puerto Rico PRI" },
        { 634, "Qatar QAT" },
        { 638, "Réunion REU" },
        { 642, "Romania ROU" },
        { 643, "Russian Federation RUS" },
        { 646, "Rwanda RWA" },
        { 652, "Saint Barthélemy BLM" },
        { 654, "Saint Helena, Ascension and Tristan da Cunha SHN" },
        { 659, "Saint Kitts and Nevis KNA" },
        { 662, "Saint Lucia LCA" },
        { 663, "Saint Martin (French part) MAF" },
        { 666, "Saint Pierre and Miquelon SPM" },
        { 670, "Saint Vincent and the Grenadines VCT" },
        { 882, "Samoa WSM" },
        { 674, "San Marino SMR" },
        { 678, "Sao Tome and Principe STP" },
        { 682, "Saudi Arabia SAU" },
        { 686, "Senegal SEN" },
        { 688, "Serbia SRB" },
        { 690, "Seychelles SYC" },
        { 694, "Sierra Leone SLE" },
        { 702, "Singapore SGP" },
        { 534, "Sint Maarten (Dutch part) SXM" },
        { 703, "Slovakia SVK" },
        { 705, "Slovenia SVN" },
        { 90, "Solomon Islands SLB" },
        { 706, "Somalia SOM" },
        { 710, "South Africa ZAF" },
        { 239, "South Georgia and the South Sandwich Islands SGS" },
        { 728, "South Sudan SSD" },
        { 724, "Spain ESP" },
        { 144, "Sri Lanka LKA" },
        { 729, "Sudan SDN" },
        { 740, "Suriname SUR" },
        { 744, "Svalbard and Jan Mayen SJM" },
        { 748, "Swaziland SWZ" },
        { 752, "Sweden SWE" },
        { 756, "Switzerland CHE" },
        { 760, "Syrian Arab Republic SYR" },
        { 158, "Taiwan, Province of China TWN" },
        { 762, "Tajikistan TJK" },
        { 834, "Tanzania, United Republic of TZA" },
        { 764, "Thailand THA" },
        { 626, "Timor-Leste TLS" },
        { 768, "Togo TGO" },
        { 772, "Tokelau TKL" },
        { 776, "Tonga TON" },
        { 780, "Trinidad and Tobago TTO" },
        { 788, "Tunisia TUN" },
        { 792, "Turkey TUR" },
        { 795, "Turkmenistan TKM" },
        { 796, "Turks and Caicos Islands TCA" },
        { 798, "Tuvalu TUV" },
        { 800, "Uganda UGA" },
        { 804, "Ukraine UKR" },
        { 784, "United Arab Emirates ARE" },
        { 826, "United Kingdom of Great Britain and Northern Ireland GBR" },
        { 840, "United States of America USA" },
        { 581, "United States Minor Outlying Islands UMI" },
        { 858, "Uruguay URY" },
        { 860, "Uzbekistan UZB" },
        { 548, "Vanuatu VUT" },
        { 862, "Venezuela (Bolivarian Republic of) VEN" },
        { 704, "Viet Nam VNM" },
        { 92, "Virgin Islands (British) VGB" },
        { 850, "Virgin Islands (U.S.) VIR" },
        { 876, "Wallis and Futuna WLF" },
        { 732, "Western Sahara ESH" },
        { 887, "Yemen YEM" },
        { 894, "Zambia ZMB" },
        { 716, "Zimbabwe ZWE" },
    };
}
