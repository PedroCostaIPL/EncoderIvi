using EncoderIvi.Message;
using PerEncDec.IVI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi
{
    public class Object2Byte
    {
        public static void ObjectTransform(PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIVI, string headerType, string ivimID)
        {
            //Getting ivi byte array 
            PerUnalignedCodec codec = new PerUnalignedCodec();
            byte[] ivi = codec.Encode(rootIVI);
            byte[] final = new byte[0];

            //Getting final byte array 
            byte[] header = new byte[0];
            if (headerType.Equals("GeoNetworking"))
            {
                string path = @"GEOeBTPBytes.dat";
                 header = File.ReadAllBytes(path);

                final = new byte[header.Length + ivi.Length];
                System.Buffer.BlockCopy(header, 0, final, 0, header.Length);
                System.Buffer.BlockCopy(ivi, 0, final, header.Length, ivi.Length);

                //Creating byte file
                File.WriteAllBytes("GeoBtpIvim_" + ivimID + ".ivi", final);
                MessageBox.Show("File created");
            }
            else {
                final = ivi;

                //Creating byte file
                File.WriteAllBytes("Ivim_" + ivimID + ".ivi", final);
                MessageBox.Show("File created");
            }


        }

    }
}
