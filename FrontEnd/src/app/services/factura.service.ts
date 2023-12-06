import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertService } from '@shared/services/alert.service';
import { CommonApi } from '../responses/common/common.response';
import { Observable } from 'rxjs';
import { environment as env } from "src/environments/environment";
import { endpointFacturacion } from '@shared/apis/endpoint';
import { ListFacturaRequest } from '../requests/category/list-factura.request';
import { map } from 'rxjs/operators';
import { ApiResponse } from '../commons/response.interface';
import { FacturaRequest } from '../requests/factura/factura.request';

@Injectable({
  providedIn: 'root'
})
export class FacturaService {

  constructor(private http: HttpClient, private alert: AlertService) { }
  GetAll(size, sort, order, page, getInputs): Observable<CommonApi> {
    const params: ListFacturaRequest = new ListFacturaRequest(
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
    const requestUrl = `${env.api}${endpointFacturacion.ALL}${params.toQueryString()}`
    
    return this.http.get<CommonApi>(requestUrl).pipe(
      map((data: CommonApi) => {
        data.data.items.forEach(function (e: any) {
          switch (e.Estado) {
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
  CategoryRegister(category: FacturaRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpointFacturacion.COMMON}`
    return this.http.post(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  FacturaById(id: number): Observable<FacturaDetallada> {
    const requesUrl = `${env.api}${endpointFacturacion.COMMON}${id}`
    return this.http.get(requesUrl).pipe(
      map((resp: ApiResponse) => resp.data)
    )
  }
  FacturaEdit(id: number, category: FacturaRequest): Observable<ApiResponse> {
    const requestUrl = `${env.api}${endpointFacturacion.COMMON}${id}`
    return this.http.put(requestUrl, category).pipe(
      map((resp: ApiResponse) => resp)
    )
  }
  CategoryRemove(id: number): Observable<void> {
    const requesUrl = `${env.api}${endpointFacturacion.COMMON}${id}`
    return this.http.delete(requesUrl).pipe(
      map((resp: ApiResponse) => {
        if (resp.isSucces) this.alert.success('Excelente', resp.message)
      })
    )
  }
}
