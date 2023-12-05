import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertService } from '@shared/services/alert.service';
import { Observable } from 'rxjs';
import { Category, CategoryApi } from '../responses/category/category.response';
import { environment as env } from "src/environments/environment";
import { endpoint } from '@shared/apis/endpoint';
import { ListCategoryRequest } from '../requests/category/list-category.request';
import { map } from 'rxjs/operators';
import { CategoryRequest } from '../requests/category/category.request';
import { ApiResponse } from '../commons/response.interface';
import { th } from 'date-fns/locale';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, private alert: AlertService) {
  }
  GetAll(size, sort, order, page, getInputs): Observable<CategoryApi> {
    const requestUrl = `${env.api}${endpoint.LIST_CATEGORIES}`
    const params: ListCategoryRequest = new ListCategoryRequest(
      page + 1,
      order,
      sort,
      size,
      getInputs.numFilter,
      getInputs.textFilter,
      getInputs.stateFilter,
      getInputs.startDate,
      getInputs.endDate
    )
    return this.http.post<CategoryApi>(requestUrl, params).pipe(
      map((data: CategoryApi) => {
        data.data.items.forEach(function (e: any) {
          switch (e.state) {
            case 0:
              e.badgeColor = 'text-gray bg-gray-light'
              break;
            case 1:
              e.badgeColor = 'text-green bg-green-light'
              break;
            default:
              e.badgeColor = 'text-gray bg-gray-light'
              break;
          }
        });
        return data;
      })
    )
  }
  CategoryRegister(category: CategoryRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.CATEGORY_REGISTER}`
    return this.http.post(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  CategoryById(id: number): Observable<Category> {
    const requesUrl = `${env.api}${endpoint.CATEGORY_BY_ID}${id}`
    return this.http.get(requesUrl).pipe(
      map((resp: ApiResponse) => resp.data)
    )
  }
  CategoryEdit(id: number, category: CategoryRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.CATEGORY_EDIT}${id}`
    return this.http.put(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  CategoryRemove(id: number): Observable<void> {
    const requesUrl = `${env.api}${endpoint.CATEGORY_REMOVE}${id}`
    return this.http.delete(requesUrl).pipe(
      map((resp: ApiResponse) => {
        if (resp.isSucces) this.alert.success('Excelente', resp.message)
      })
    )
  }
}
