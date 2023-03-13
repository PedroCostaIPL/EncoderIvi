using Newtonsoft.Json;
using PerEncDec.IVI;
using System.Collections;
using System.Net.Http.Headers;
using EncoderIvi.Message;
using PerEncDec.IVI.IVIModule;

namespace EncoderIvi
{
    public static class Json2PerBitAdapter
    {
        public const double COEFFICIENT = 1000000;

        public static async void MakeRequest(string jsonID)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var countryCodeBits = new BitArray(10);
            countryCodeBits.Set(0, false);
            countryCodeBits.Set(1, false);
            countryCodeBits.Set(2, false);
            countryCodeBits.Set(3, false);
            countryCodeBits.Set(4, false);
            countryCodeBits.Set(5, false);
            countryCodeBits.Set(6, false);
            countryCodeBits.Set(7, false);
            countryCodeBits.Set(8, false);
            countryCodeBits.Set(9, false);

            await JsonHttpRequest(client);

            async Task JsonHttpRequest(HttpClient client)
            {
                String myJsonResponse = await client.GetStringAsync(
                    "http://projeto-informatico2.test/api/ivim/json/" + jsonID);

                Root deserializedJson = JsonConvert.DeserializeObject<Root>(myJsonResponse);

                PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIVI = new PerEncDec.IVI.IVIMPDUDescriptions.IVIM();
                //Header
                rootIVI.Header = new PerEncDec.IVI.ITSContainer.ItsPduHeader();
                rootIVI.Header.MessageID = deserializedJson.data.header.messageID;
                rootIVI.Header.StationID = deserializedJson.data.header.stationID;
                rootIVI.Header.ProtocolVersion = deserializedJson.data.header.protocolVersion;

                //Mandatory
                rootIVI.IviField = new PerEncDec.IVI.IVIModule.IviStructure();
                rootIVI.IviField.Mandatory = new PerEncDec.IVI.IVIModule.IviManagementContainer();
                rootIVI.IviField.Mandatory.ServiceProviderId = new PerEncDec.IVI.EfcDsrcApplication.Provider();
                rootIVI.IviField.Mandatory.ServiceProviderId.CountryCode = new PerEncDec.Asn1.BitString(countryCodeBits);
                rootIVI.IviField.Mandatory.ServiceProviderId.ProviderIdentifier = deserializedJson.data.ivi[0].mandatory.serviceProviderId.providerIdentifier;
                rootIVI.IviField.Mandatory.IviIdentificationNumber = deserializedJson.data.ivi[0].mandatory.iviIdentificationNumber;
                rootIVI.IviField.Mandatory.TimeStamp = deserializedJson.data.ivi[0].mandatory.timeStamp;
                rootIVI.IviField.Mandatory.ValidFrom = deserializedJson.data.ivi[0].mandatory.validFrom;
                rootIVI.IviField.Mandatory.ValidTo = deserializedJson.data.ivi[0].mandatory.validTo;

                if (deserializedJson.data.ivi[0].mandatory.connectedIviStructures is not null) {
                    rootIVI.IviField.Mandatory.ConnectedIviStructures.Capacity = (int)deserializedJson.data.ivi[0].mandatory.connectedIviStructures;
                }
                rootIVI.IviField.Mandatory.IviStatus = (int)deserializedJson.data.ivi[0].mandatory.iviStatus;

                //Optional
                rootIVI.IviField.Optional = new PerEncDec.IVI.IVIModule.IviContainers
                {
                    new PerEncDec.IVI.IVIModule.IviContainer(),new PerEncDec.IVI.IVIModule.IviContainer(),new PerEncDec.IVI.IVIModule.IviContainer()
                };

                //Glc
                rootIVI.IviField.Optional[0].Glc = new PerEncDec.IVI.IVIModule.GeographicLocationContainer();
                rootIVI.IviField.Optional[0].Glc.ReferencePosition = new PerEncDec.IVI.ITSContainer.ReferencePosition();
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.Latitude = (long) (deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.lat * COEFFICIENT);
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.Longitude = (long) (deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.lng * COEFFICIENT);
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse = new PerEncDec.IVI.ITSContainer.PosConfidenceEllipse();
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMajorConfidence = deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMajorConfidence;
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMinorConfidence = deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMinorConfidence;
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMajorOrientation = deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMajorOrientation;
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude = new PerEncDec.IVI.ITSContainer.Altitude();
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude.AltitudeValue = deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.altitude.altitudeValue;
                rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude.AltitudeConfidence = (PerEncDec.IVI.ITSContainer.AltitudeConfidence)deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePosition.altitude.altitudeConfidence;
                rootIVI.IviField.Optional[0].Glc.ReferencePositionTime = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.glc.referencePositionTime;

                //Glc//Parts
                rootIVI.IviField.Optional[0].Glc.Parts = new PerEncDec.IVI.IVIModule.GlcParts();
                foreach (Message.GlcPart glcPart in deserializedJson.data.ivi[0].optional[0].IviContainer.glc.parts.GlcPart)
                {
                    PerEncDec.IVI.IVIModule.GlcPart newPart = new PerEncDec.IVI.IVIModule.GlcPart();
                    newPart.ZoneId = glcPart.zoneId;
                    newPart.LaneNumber = (long?)glcPart.laneNumber;
                    newPart.ZoneExtension = (int?)glcPart.zoneExtension;
                    newPart.ZoneHeading = (int?)glcPart.zoneHeading;
                    newPart.Zone = new PerEncDec.IVI.IVIModule.Zone();
                    newPart.Zone.Segment = new PerEncDec.IVI.IVIModule.Segment();
                    newPart.Zone.Segment.Line = new PerEncDec.IVI.IVIModule.PolygonalLine();

                    //DeltaPosition
                    if (glcPart.zone.segment.line.deltaPositions is not null) { 
                        newPart.Zone.Segment.Line.DeltaPositions = new PerEncDec.IVI.IVIModule.DeltaPositions();
                        foreach (Message.DeltaPosition pairCoordenates in glcPart.zone.segment.line.deltaPositions.DeltaPosition)
                        {
                            PerEncDec.IVI.IVIModule.DeltaPosition? newCoord = new PerEncDec.IVI.IVIModule.DeltaPosition();
                            newCoord.DeltaLatitude = (long) pairCoordenates.deltaLatitude;
                            newCoord.DeltaLongitude = (long) pairCoordenates.deltaLongitude;
                            newPart.Zone.Segment.Line.DeltaPositions.Add(newCoord);
                        }
                    }
                    //AbsolutePosition
                    else if (glcPart.zone.segment.line.absolutePositions is not null)
                    {
                        newPart.Zone.Segment.Line.AbsolutePositions = new PerEncDec.IVI.IVIModule.AbsolutePositions();
                        foreach (Message.AbsolutePosition pairCoordenates in glcPart.zone.segment.line.absolutePositions.AbsolutePosition)
                        {
                            PerEncDec.IVI.IVIModule.AbsolutePosition? newCoord = new PerEncDec.IVI.IVIModule.AbsolutePosition();
                            newCoord.Latitude = (long) (pairCoordenates.latitude * COEFFICIENT);
                            newCoord.Longitude = (long) (pairCoordenates.longitude * COEFFICIENT);
                            newPart.Zone.Segment.Line.AbsolutePositions.Add(newCoord);
                        }
                    }

                    newPart.Zone.Segment.LaneWidth = (int?)glcPart.zone.segment.laneWidth;
                    
                    rootIVI.IviField.Optional[0].Glc.Parts.Add(newPart);
                }

                //Giv
                rootIVI.IviField.Optional[1].Giv = new PerEncDec.IVI.IVIModule.GeneralIviContainer
                {
                    new PerEncDec.IVI.IVIModule.GicPart()
                };

                rootIVI.IviField.Optional[1].Giv[0].DetectionZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
                foreach (DetectionZoneId detectionZoneId in deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.detectionZoneIds)
                {
                    rootIVI.IviField.Optional[1].Giv[0].DetectionZoneIds.Add(detectionZoneId.Zid);
                }

                rootIVI.IviField.Optional[1].Giv[0].RelevanceZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
                foreach (RelevanceZoneId relevanceZoneId in deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.relevanceZoneIds)
                {
                    rootIVI.IviField.Optional[1].Giv[0].RelevanceZoneIds.Add(relevanceZoneId.Zid);
                }

                rootIVI.IviField.Optional[1].Giv[0].Direction = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.direction;
                rootIVI.IviField.Optional[1].Giv[0].MinimumAwarenessTime = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.minimumAwarenessTime;
                rootIVI.IviField.Optional[1].Giv[0].IviType = (int)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.iviType;
                rootIVI.IviField.Optional[1].Giv[0].IviPurpose = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.iviPurpose;
                rootIVI.IviField.Optional[1].Giv[0].LaneStatus = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.laneStatus;
                rootIVI.IviField.Optional[1].Giv[0].DriverCharacteristics = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.driverCharacteristics;
                rootIVI.IviField.Optional[1].Giv[0].LayoutId = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.layoutId;
                rootIVI.IviField.Optional[1].Giv[0].PreStoredlayoutId = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.preStoredlayoutId;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes = new PerEncDec.IVI.IVIModule.RoadSignCodes
                {
                    new PerEncDec.IVI.IVIModule.RSCode()
                };
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].LayoutComponentId = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.layoutComponentId;

                //MessageBox.Show(deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.serviceCategoryCode.trafficSignPictogram.ToString());
                
                //Giv//ViennaConvention
                if (deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention is not null)
                {
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention = new VcCode();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.RoadSignClass = (int)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.roadSignClass;
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.RoadSignCode = (int)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.roadSignCode;
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.VcOption = (int)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.vcOption;
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.Value = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.value;
                }
                //Giv//Iso14823
                else if (deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823 is not null)
                {
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823  = new ISO14823Code();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode = new ISO14823Code.PictogramCodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.CountryCode = null;  //new PerEncDec.Asn1.BitString(countryCodeBits);
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.ServiceCategoryCode = new ISO14823Code.PictogramCodeType.ServiceCategoryCodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.ServiceCategoryCode.TrafficSignPictogram = (ISO14823Code.PictogramCodeType.ServiceCategoryCodeType.TrafficSignPictogramType?)deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.serviceCategoryCode.trafficSignPictogram;
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode = new ISO14823Code.PictogramCodeType.PictogramCategoryCodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode.Nature = deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.pictogramCategoryCode.nature;
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode.SerialNumber = deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.pictogramCategoryCode.serialNumber;
                }
                //Giv//ItsCodes
                else if (deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.itisCode is not null)
                {
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ItisCodes = deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.itisCode;
                }

                rootIVI.IviField.Optional[1].Giv[0].ExtraText = new PerEncDec.IVI.IVIModule.ConstraintTextLines1
                {
                    new PerEncDec.IVI.IVIModule.Text()
                };
                rootIVI.IviField.Optional[1].Giv[0].ExtraText[0].Language = new PerEncDec.Asn1.BitString(10);
                rootIVI.IviField.Optional[1].Giv[0].ExtraText[0].TextContent = deserializedJson.data.ivi[0].optional[0].IviContainer.giv[0].GicPart.extraText.ToString();


                //Tc
                rootIVI.IviField.Optional[2].Tc = new PerEncDec.IVI.IVIModule.TextContainer
                {                   
                    new PerEncDec.IVI.IVIModule.TcPart()
                };

                rootIVI.IviField.Optional[2].Tc[0].DetectionZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
                foreach (DetectionZone detectionZone in deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.DetectionZone)
                {
                    rootIVI.IviField.Optional[2].Tc[0].DetectionZoneIds.Add(detectionZone.Zid);
                }

                rootIVI.IviField.Optional[2].Tc[0].RelevanceZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
                foreach (RelevanceZone relevanceZone in deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.RelevanceZone)
                {
                    rootIVI.IviField.Optional[2].Tc[0].RelevanceZoneIds.Add(relevanceZone.Zid);
                }

                rootIVI.IviField.Optional[2].Tc[0].Direction = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.Direction;
                rootIVI.IviField.Optional[2].Tc[0].MinimumAwarenessTime = (int?)deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.minimumAwarenessZoneIds;
                rootIVI.IviField.Optional[2].Tc[0].LayoutId = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.layoutId;
                rootIVI.IviField.Optional[2].Tc[0].PreStoredlayoutId = (long?)deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.preStoredlayoutId;
                rootIVI.IviField.Optional[2].Tc[0].Text = new PerEncDec.IVI.IVIModule.TextLines();
                //rootIVI.IviField.Optional[0].Tc[0].Text = deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.text;
                rootIVI.IviField.Optional[2].Tc[0].Data = BitConverter.GetBytes(deserializedJson.data.ivi[0].optional[0].IviContainer.tc[0].TcPart.data.Value.Ticks);


                PerUnalignedCodec codec = new PerUnalignedCodec();
                byte[] ivib = codec.Encode(rootIVI);
                PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIvi2 = new PerEncDec.IVI.IVIMPDUDescriptions.IVIM();
                codec.Decode(ivib, 0, rootIvi2);
            }
        }     
    }
}
