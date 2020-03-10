import { isNullOrUndefined } from 'util';

export class PageParams {
    PageNumber: number;
    PageSize: number;
    Filter?;
    constructor(
        pageSize: number,
        pageNumber: number,
        filter?,
    ) {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        if (!(isNullOrUndefined(filter) || '')) {
            this.Filter = filter;
        }

    }
}
