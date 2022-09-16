import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DestinationsComponent } from './destinations/destinations.component';
import { HomeComponent } from './home/home.component';
import { RegisterDestinationsComponent } from './register-destinations/register-destinations.component';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'register', component:RegisterDestinationsComponent},
  {path:'destinations', component:DestinationsComponent},
  {path:'**', component:HomeComponent, pathMatch:'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
