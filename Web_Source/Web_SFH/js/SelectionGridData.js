function SelectionGridData(data) {
    this.Grid = {
        url: '',
        data: data,
        width: 1000,
        widthPlus: 110,
        searchDate: {},
        popup: false,
        id: 'ID',
        page_navigate:'selections',
        columns: [
            {
                value: 'ID',
                field: 'ID',
                title: 'Gen ID',
                width: 120,
                widthPlus: 2,
                sortable: false,
                filterable: false,               
                template: "",
                naviagte: false
            },
            {
                value: 'RunOn',
                field: 'RunOn',
                title: 'Run On',
                width: 160,
                widthPlus: 2,
                sortable: false,
                filterable: false,
                datatype: 'Datetime',
                dataformat: 'MM/dd/yyyy HH:mm:ss',
                template: "",
                naviagte: false
            },
            {
                value: 'Status',
                field: 'Status',
                title: 'Status',
                width: 100,
                widthPlus: 2,
                sortable: false,
                filterable: false,
                bit: true,
                bitvalue:"Pending|Complete",
                template: "",
                naviagte: false
            },
            {
                value: 'Name',
                field: 'Name',
                title: 'Instance',
                width: 440,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                template: ""
            },
            {
                value: 'DonorSpecimenList',
                field: 'DonorSpecimenList',
                title: 'Eligible',
                width: 80,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                isCount: true,
                template: "",
                naviagte: false
            },
            {
                value: 'DonorSpecimenList',
                field: 'DonorSpecimenList',
                title: 'Test',
                width: 160,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                isMethod: true,
                method:"Test",
                template: "",
                naviagte: false
            },
            //{
            //    value: '',
            //    field: '',
            //    title: 'Actions',
            //    width: 80,
            //    widthPlus: 2,
            //    sortable: false,
            //    filterable: false,
            //    hidden: true,
            //    template: "<p id='del#ID#' class='delete' ></p>"
            //},
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