function SelectionDonorGridData(data) {
    this.Grid = {
        url: '',
        data: data,
        width: 1200,
        widthPlus: 0,
        searchDate: {},
        popup: false,
        id: 'ID',        
        columns: [
            {
                value: '',
                field: '',
                title: 'No',
                width: 80,
                widthPlus: 2,
                sortable: false,
                filterable: false,
                template: "",
                navigate: false
            },
            {
                value: 'FullName',
                field: 'FullName',
                title: 'Donor',
                width: 200,
                widthPlus: 2,
                sortable: false,
                filterable: false,                
                template: "",
                navigate: false
            },
            {
                value: 'PrimaryID',
                field: 'PrimaryID',
                title: 'Unique Id',
                width: 140,
                widthPlus: 2,
                sortable: false,
                filterable: false,                
                navigate: false
            }, 
            {
                value: 'DonorID',
                field: 'DonorID',
                title: 'Birthday',
                width: 120,
                widthPlus: 2,
                sortable: false,
                filterable: false,
                isMethod: true,
                method: "RetDonorBirthday"
            },
            {
                value: 'DonorID',
                field: 'DonorID',
                title: 'Mobile',
                width: 120,
                widthPlus: 2,
                sortable: false,
                filterable: false,               
                isMethod: true,
                method:"RetDonorMobilePhone"
            }, 

            {
                value: 'CompanyName',
                field: 'CompanyName',
                title: 'Company',
                width: 300,
                widthPlus: 2,
                sortable: false,
                filterable: false,
                navigate: false
            },  
            {
                value: 'Specimen1|Specimen2',
                field: 'Specimen1',
                title: 'Specimen',
                width: 140,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                //isMethod: true,
                //method: "Specimen",
                template: "",
                navigate: false
            },           
            {
                field: '',
                title: '',
                width: 5,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            }

        ]
    }
}