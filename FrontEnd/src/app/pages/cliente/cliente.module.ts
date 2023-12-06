import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClienteRoutingModule } from './cliente-routing.module';
import { ClienteListComponent } from './cliente-list/cliente-list.component';
import { SharedModule } from '@shared/shared.module';
import { ClienteManageComponent } from './cliente-manage/cliente-manage.component';


@NgModule({
  declarations: [
    ClienteListComponent,
    ClienteManageComponent
  ],
  imports: [
    CommonModule,
    ClienteRoutingModule,
    SharedModule
  ]
})
export class ClienteModule { }
