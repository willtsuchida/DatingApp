export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    result?: T;
    pagination?: Pagination;
    //in order to use this when we get our response back from the api we need to look the header
    //fish out the pagination information and create a new PaginatedResult<T> class, popular a pagination (prop dessa classe)...

}