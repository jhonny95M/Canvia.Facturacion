import { TableColumn } from "src/@vex/interfaces/table-column.interface";
import { Category } from "src/app/responses/category/category.response";
import icCategory from "@iconify/icons-ic/twotone-category";
import { ListTableMenu } from "src/app/commons/list-table-menu.interface";
import icViewHeadline from "@iconify/icons-ic/twotone-view-headline";
import icLabel from "@iconify/icons-ic/twotone-label";
import icCalendarMonth from "@iconify/icons-ic/twotone-calendar-today";
import { GenericValidators } from "@shared/validators/generic-validators";

const searchOptions=[
    {
        label:'Nombre',
        value:1,
        placeholder:"Buscar por nombre",
        validation:[GenericValidators.defaultName],
        validation_desc:"Solo se permite  letras en esta busqueda",
        min_length:2
    },
    {
        label:'Descripcion',
        value:2,
        placeholder:"Buscar por descripcion",
        validation:[GenericValidators.defaultDescription],
        validation_desc:"Solo se permite  letras y numeros en esta busqueda",
        min_length:2
    }
]
const menuItems:ListTableMenu[]=[
    {
        type:'link',
        id:'all',
        icon:icViewHeadline,
        label:'Todos'
    },
    {
        type:'link',
        id:'Activo',
        value:1,
        icon:icLabel,
        label:'Activo',
        classes:{
            icon:"text-green"
        }
    },
    {
        type:'link',
        id:'Inactivo',
        value:0,
        icon:icLabel,
        label:'Incativo',
        classes:{
            icon:"text-gray"
        }
    }
]
const tableColumns:TableColumn<Category>[]=[
    {
        label:'Nombre',
        property:'name',
        type:'text',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'Descripción',
        property:'description',
        type:'textTruncate',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'F. Creación',
        property:'auditCreateDate',
        type:'datetime',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'Estado',
        property:'stateCategory',
        type:'badge',
        cssClasses:['font-medium','w-10']
    },
    {
        label:'',
        property:'menu',
        type:'buttonGroup',
        buttonItems:[
            {
                buttonLabel:'EDITAR',
                buttonAction:'edit',
                buttonCondition:null,
                disable:false
            },
            {
                buttonLabel:'ELIMINAR',
                buttonAction:'remove',
                buttonCondition:null,
                disable:false
            }
        ],
        cssClasses:['font-medium','w-10']
    }
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
    searchOptions:searchOptions,
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