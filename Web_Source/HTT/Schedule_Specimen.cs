using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HTT
{
    public class Schedule_Specimen
    {
        #region Fields
        private String sType = "", sPanel = "", sCollectionSite = "", sMRO = "", sLab = "";
        private double iNumberDonor = 0;
        private int iNumberDonorType = 0,iSelectionMethod=0;
        private double iAlternateNumberDonor = 0;
        private int iAlternateNumberDonorType = 0, iAlternateSelectionMethod = 0;
        #endregion End Fields

        #region Constructors
        public Schedule_Specimen()
        {
            
        }

        public Schedule_Specimen(String type,String plan,String collectionSite,String mro,String lab,int numberDonor,int numberDonorType,int selectionMethod)
        {
            this.sType = type;
            this.sPanel = plan;
            this.sCollectionSite = collectionSite;
            this.sMRO = mro;
            this.sLab = lab;
            this.iNumberDonor = numberDonor;
            this.iNumberDonorType = numberDonorType;
            this.iSelectionMethod = selectionMethod;

        }

        public Schedule_Specimen(HttpRequest request,int type)
        {
            if (type.Equals(1))
            {
                this.sType = Lib.get_value_str(request[FieldKeys.SpecimenType1]);
                this.sPanel = Lib.get_value_str(request[FieldKeys.Plan]);
                this.sCollectionSite = Lib.get_value_str(request[FieldKeys.CollectionSite1]);
                this.sMRO = Lib.get_value_str(request[FieldKeys.MRO1]);
                this.sLab = Lib.get_value_str(request[FieldKeys.Lab1]);
                this.iNumberDonor = Lib.get_value_double(request[FieldKeys.NumberDonor1]);
                this.iNumberDonorType = Lib.get_value_int(request[FieldKeys.NumberDonorType1]);
                this.iSelectionMethod = Lib.get_value_int(request[FieldKeys.SelectionMethod1]);
                this.iAlternateNumberDonor = Lib.get_value_double(request[FieldKeys.AlternateNumberDonor1]);
                this.AlternateNumberDonorType = Lib.get_value_int(request[FieldKeys.AlternateNumberDonorType1]);
            }else
            {
                this.sType = Lib.get_value_str(request[FieldKeys.SpecimenType2]);
                this.sPanel = Lib.get_value_str(request[FieldKeys.Plan]);
                this.sCollectionSite = Lib.get_value_str(request[FieldKeys.CollectionSite2]);
                this.sMRO = Lib.get_value_str(request[FieldKeys.MRO2]);
                this.sLab = Lib.get_value_str(request[FieldKeys.Lab2]);
                this.iNumberDonor = Lib.get_value_double(request[FieldKeys.NumberDonor2]);
                this.iNumberDonorType = Lib.get_value_int(request[FieldKeys.NumberDonorType2]);
                this.iSelectionMethod = Lib.get_value_int(request[FieldKeys.SelectionMethod2]);
                this.iAlternateNumberDonor = Lib.get_value_double(request[FieldKeys.AlternateNumberDonor2]);
                this.AlternateNumberDonorType = Lib.get_value_int(request[FieldKeys.AlternateNumberDonorType2]);
            }
                       

        }



        public string Type { get => sType; set => sType = value; }
        public string Panel { get => sPanel; set => sPanel = value; }
        public string CollectionSite { get => sCollectionSite; set => sCollectionSite = value; }
        public string MRO { get => sMRO; set => sMRO = value; }
        public string Lab { get => sLab; set => sLab = value; }
        public double NumberDonor { get => iNumberDonor; set => iNumberDonor = value; }
        public int NumberDonorType { get => iNumberDonorType; set => iNumberDonorType = value; }
        public int SelectionMethod { get => iSelectionMethod; set => iSelectionMethod = value; }
        public double AlternateNumberDonor { get => iAlternateNumberDonor; set => iAlternateNumberDonor = value; }
        public int AlternateNumberDonorType { get => iAlternateNumberDonorType; set => iAlternateNumberDonorType = value; }
        public int AlternateSelectionMethod { get => iAlternateSelectionMethod; set => iAlternateSelectionMethod = value; }
        #endregion End Constructors

        #region Methods
        #endregion
    }
}
