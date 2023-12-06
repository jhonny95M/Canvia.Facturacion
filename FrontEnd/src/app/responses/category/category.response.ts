import { CommonApi } from "../common/common.response";

export interface Category{
    categoryId: number;
    name: string;
    description: string;
    auditCreateDate: Date;
    state: number;
    stateCategory?: string;
}
export interface CategoryApi extends CommonApi
{
    // data:any
    // totalRecords:number
}