import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import{BrowserAnimationsModule} from '@angular/platform-browser/animations';
import{FormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { RegisterDestinationsComponent } from './destinations/register-destinations/register-destinations.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { DestinationsListComponent } from './destinations/destinations-list/destinations-list.component';
import { DestinationCardComponent } from './destinations/destination-card/destination-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { DestinationDetailComponent } from './destinations/destination-detail/destination-detail.component';
import { DestinationEditComponent } from './destinations/destination-edit/destination-edit.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoDetinationEditorComponent } from './destinations/photo-detination-editor/photo-detination-editor.component';
import { DescriptionComponent } from './destinations/description/description.component';
import { PromotionCardComponent } from './promotions/promotion-card/promotion-card.component';
import { PromotionDetailComponent } from './promotions/promotion-detail/promotion-detail.component';
import { PromotionsListComponent } from './promotions/promotions-list/promotions-list.component';
import { PromotionEditComponent } from './promotions/promotion-edit/promotion-edit.component';
import { PromotionDescriptionComponent } from './promotions/promotionDescription/promotionDescription.component';
import { PhotoPromotionEditorComponent } from './promotions/photo-promotion-editor/photo-promotion-editor.component';
import { RegisterPromotionsComponent } from './promotions/register-promotions/register-promotions.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginFormComponent,
    HomeComponent,
    RegisterDestinationsComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    DestinationsListComponent,
    DestinationCardComponent,
    DestinationDetailComponent,
    DestinationEditComponent,
    PhotoDetinationEditorComponent,
    DescriptionComponent,
    PromotionCardComponent,
    PromotionDetailComponent,
    PromotionsListComponent,
    PromotionEditComponent,
    PromotionDescriptionComponent,
    PhotoPromotionEditorComponent,
    RegisterPromotionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    NgxSpinnerModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS, useClass:ErrorInterceptor, multi: true},
    {provide:HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi: true},
    {provide:HTTP_INTERCEPTORS, useClass:LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
