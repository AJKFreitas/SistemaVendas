import { PageData } from './PageData';

export class ResponseData<T> {
    pageData: PageData;
    data: Array<T>;
  }