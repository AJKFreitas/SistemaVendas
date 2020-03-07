import { Observable } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';

export class HttpPaginatedDataSource<T> extends MatTableDataSource<T> {
    /**
     * Custom dto containing paginated response data from a http request
     */
    private readonly paginatedQuery: {
        data$: Observable<Array<T>>;
        totalElements: number;
        // ... other metadata
    };

    // ... logic for data binding

    /**
     * Override update paginator method
     * to ensure total unfiltered element count is consistent with the http result
     */
    public _updatePaginator(filteredDataLength: number): void {
        if (this.filter === '') {
            super._updatePaginator(this.paginatedQuery.totalElements);
        } else {
            super._updatePaginator(filteredDataLength);
        }
    }
}
