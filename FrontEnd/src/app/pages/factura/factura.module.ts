import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FacturaRoutingModule } from './factura-routing.module';
import { FacturaListComponent } from './factura-list/factura-list.component';
import { SharedModule } from '@shared/shared.module';
import { FacturaManageComponent } from './factura-manage/factura-manage.component';


@NgModule({
  declarations: [
    FacturaListComponent,
    FacturaManageComponent
  ],
  imports: [
    CommonModule,
    FacturaRoutingModule,
    SharedModule
  ]
})
export class FacturaModule { }
