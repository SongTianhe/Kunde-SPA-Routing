import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Lagre } from './lagre/lagre';
import { Liste } from './liste/liste';
import { Endre } from './endre/endre';
import { Meny } from './meny/meny';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Modal } from './liste/sletteModal';

@NgModule({
  declarations: [
    AppComponent,
    Lagre,
    Liste,
    Endre,
    Meny,
    Modal
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [Modal] // merk!  
})
export class AppModule { }
