import { convertDateToRequest } from "@shared/functions/helpers"
import { params } from "src/app/commons/params-api.interface"

export class ListFacturaRequest extends params {
    constructor(
        public readonly numpage: number,
        public readonly order: 'desc' | 'asc',
        public readonly sort: string,
        public readonly records: 10 | 20 | 50,
        public readonly numFilter: number = 0,
        public readonly textFilter: string = "",
        public readonly stateFilter: number = null,
        private startDate: string,
        private endDate: string
    ) {
        super(true, numpage, order, sort, records, false, numFilter, textFilter, stateFilter)
        this.startDate = convertDateToRequest(this.startDate, 'date')
        this.endDate = convertDateToRequest(this.endDate, 'date')
    }
    toQueryString(): string {
        const queryParams = new URLSearchParams();
        queryParams.set('numpage', this.numpage.toString());
        queryParams.set('order', this.order);
        queryParams.set('sort', this.sort);
        queryParams.set('records', this.records.toString());
        queryParams.set('numFilter', this.numFilter.toString());
        if(this.textFilter!=null && this.textFilter!='')
        queryParams.set('textFilter', this.textFilter);
        if (this.stateFilter !== null) 
            queryParams.set('stateFilter', this.stateFilter.toString())
        if(this.startDate!=null)
        queryParams.set('startDate', this.startDate);
        if(this.endDate!=null)
        queryParams.set('endDate', this.endDate);

        return queryParams.toString();
    }
}