import { TableColumn } from "src/@vex/interfaces/table-column.interface";
import { Cliente } from "src/app/responses/category/category.response";
import icCategory from "@iconify/icons-ic/twotone-category";
import { ListTableMenu } from "src/app/commons/list-table-menu.interface";
import icViewHeadline from "@iconify/icons-ic/twotone-view-headline";
import icLabel from "@iconify/icons-ic/twotone-label";
import icCalendarMonth from "@iconify/icons-ic/twotone-calendar-today";
import { GenericValidators } from "@shared/validators/generic-validators";


const menuItems:ListTableMenu[]=[
    {
        type:'link',
        id:'all',
        icon:icViewHeadline,
        label:'Todos'
    }
]
const tableColumns:TableColumn<Cliente>[]=[
    {
        label:'ID',
        property:'clienteID',
        type:'textTruncate',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'Nombres',
        property:'nombre',
        type:'textTruncate',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'Apellidos',
        property:'apellido',
        type:'textTruncate',
        cssClasses:['font-medium','w-10']
    }
    // {
    //     label:'',
    //     property:'menu',
    //     type:'buttonGroup',
    //     buttonItems:[
    //         {
    //             buttonLabel:'EDITAR',
    //             buttonAction:'edit',
    //             buttonCondition:null,
    //             disable:false
    //         },
    //         {
    //             buttonLabel:'ELIMINAR',
    //             buttonAction:'remove',
    //             buttonCondition:null,
    //             disable:false
    //         }
    //     ],
    //     cssClasses:['font-medium','w-10']
    // }
    
]
const filters={
    numFilter:0,
    textFilter:"",
    stateFilter:null,
    startDate:null,
    endDate:null
}
const inputs={
    numFilter:0,
    textFilter:"",
    stateFilter:null,
    startDate:null,
    endDate:null
}
export const componentSettings={
    //icons
    icCategory:icCategory,
    icCalendarMonth:icCalendarMonth,
    //layout settings
    menuOpen:false,
    //table settings
    tableColumns:tableColumns,
    initialSort:"Id",
    initialSortDir:"desc",
    getInputs:inputs,
    buttonLabel:"EDITAR",
    buttonLabel2:"ELIMINAR",
    //search filters
    menuItems:menuItems,
    filters:filters,
    filters_dates_active:false,
    datesFilterArray:['Fecha de creacion'],
    columnsFilter:tableColumns.map((column)=>{
        return {
            label:column.label,
            property:column.property,
            type:column.type
        }
    })
}