import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VexModule } from '../@vex/vex.module';
import { HttpClientModule } from '@angular/common/http';
import { CustomLayoutModule } from './custom-layout/custom-layout.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CustomLayoutAuthComponent } from './custom-layout-auth/custom-layout-auth.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { CUSTOM_DATE_FORMATS } from 'src/static-data/configs';
import { ReactiveFormsModule } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button'

@NgModule({
  declarations: [AppComponent, NotFoundComponent, CustomLayoutAuthComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatSnackBarModule,
    NgxSpinnerModule,
    MatInputModule,
    MatFormFieldModule,
    VexModule,
    CustomLayoutModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  bootstrap: [AppComponent],
  providers:[
    { provide: MAT_DATE_LOCALE, useValue: 'es-ES' }, // Cambia 'es-ES' por tu idioma
  { provide: MAT_DATE_FORMATS, useValue: CUSTOM_DATE_FORMATS },
  ]
})
export class AppModule { }


