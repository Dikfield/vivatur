import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { PackageComponent } from './package/package.component';
import { DestinationComponent } from './destination/destination.component';
import { FeaturesComponent } from './features/features.component';
import { AboutComponent } from './about/about.component';
import { FormComponent } from './form/form.component';
import { FooterComponent } from './footer/footer.component';
import {HttpClientJsonpModule, HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import { ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    PackageComponent,
    DestinationComponent,
    FeaturesComponent,
    AboutComponent,
    FormComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    HttpClientJsonpModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    DatePipe,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
