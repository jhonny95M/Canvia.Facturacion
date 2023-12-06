export class params {
    constructor(
        public readonly paginator: boolean,
        public readonly numPage: number,
        public readonly order: 'desc' | 'asc',
        public readonly sort: string,
        public readonly records: 10 | 20 | 50,
        public readonly download: boolean,
        public readonly numFilter: number = 0,
        public readonly textFilter: string = "",
        public readonly stateFilter: number = null,
        public readonly stateFilterTwo?: number
    ) { }
}

export interface UserToken {
    message: string;
}