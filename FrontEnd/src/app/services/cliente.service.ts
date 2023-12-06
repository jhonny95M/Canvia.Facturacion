import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertService } from '@shared/services/alert.service';
import { Observable } from 'rxjs';
import { Cliente } from '../responses/category/category.response';
import { environment as env } from "src/environments/environment";
import { endpoint } from '@shared/apis/endpoint';
import { ListCategoryRequest } from '../requests/category/list-category.request';
import { map } from 'rxjs/operators';
import { CategoryRequest } from '../requests/category/category.request';
import { ApiResponse } from '../commons/response.interface';
import { CommonApi } from '../responses/common/common.response';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  constructor(private http: HttpClient, private alert: AlertService) {
  }
  GetAll(size, sort, order, page, getInputs): Observable<CommonApi> {
    const requestUrl = `${env.api}${endpoint.LIST_CLIENTES}`
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
    return this.http.get<ApiResponse>(requestUrl).pipe(
      map((data: ApiResponse) => {
        const dataRecords:CommonApi={
          data: data.data,
          totalRecords: 1
        }
        const apiResponse:ApiResponse=
        {
          isSucces:true,
          erros:null,
          message:"Consulta existosa",
          data:dataRecords
        }
        return dataRecords
      })
    )

  }
  GetSelect(): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.LIST_SELECT_CLIENTES}`
    return this.http.get<ApiResponse>(requestUrl).pipe(
      map((data: ApiResponse) => data)
    )
  }
  CategoryRegister(category: CategoryRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.CLIENTE_REGISTER}`
    return this.http.post(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  CategoryById(id: number): Observable<Cliente> {
    const requesUrl = `${env.api}${endpoint.CLIENTE_BY_ID}${id}`
    return this.http.get(requesUrl).pipe(
      map((resp: ApiResponse) => resp.data)
    )
  }
  CategoryEdit(id: number, category: CategoryRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpoint.CLIENTE_EDIT}${id}`
    return this.http.put(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  CategoryRemove(id: number): Observable<void> {
    const requesUrl = `${env.api}${endpoint.CLIENTE_REMOVE}${id}`
    return this.http.delete(requesUrl).pipe(
      map((resp: ApiResponse) => {
        if (resp.isSucces) this.alert.success('Excelente', resp.message)
      })
    )
  }
}
