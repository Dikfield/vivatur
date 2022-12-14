import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DestinationDetailComponent } from './destinations/destination-detail/destination-detail.component';
import { DestinationEditComponent } from './destinations/destination-edit/destination-edit.component';
import { DestinationsListComponent } from './destinations/destinations-list/destinations-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { PromotionDetailComponent } from './promotions/promotion-detail/promotion-detail.component';
import { PromotionEditComponent } from './promotions/promotion-edit/promotion-edit.component';
import { PromotionsListComponent } from './promotions/promotions-list/promotions-list.component';
import { RegisterPromotionsComponent } from './promotions/register-promotions/register-promotions.component';
import { RegisterDestinationsComponent } from './destinations/register-destinations/register-destinations.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { AboutEditComponent } from './about/about-edit/about-edit.component';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {
    path:'',
    runGuardsAndResolvers: 'always',
    canActivate:[AuthGuard],
    children:[
      {path:'destinations', component:DestinationsListComponent},
      {path:'destinations/:id', component:DestinationDetailComponent},
      {path:'destination/edit/:id', component:DestinationEditComponent, canDeactivate:[PreventUnsavedChangesGuard]},
      {path:'register', component:RegisterDestinationsComponent},
      {path:'promotions', component:PromotionsListComponent},
      {path:'promotions/:id', component:PromotionDetailComponent},
      {path:'promotion/edit/:id', component:PromotionEditComponent, canDeactivate:[PreventUnsavedChangesGuard]},
      {path:'promotion/register', component:RegisterPromotionsComponent},
      {path:'about', component:AboutEditComponent}
    ]
  },
  {path:'errors', component:TestErrorsComponent},
  {path:'not-found', component:NotFoundComponent},
  {path:'server-error', component:ServerErrorComponent},
  {path:'**', component:HomeComponent, pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
