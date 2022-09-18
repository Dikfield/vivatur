import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { DestinationEditComponent } from '../destinations/destination-edit/destination-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: DestinationEditComponent):boolean {
      if(component.editForm.dirty){
        return confirm('Você tem certeza em continuar? As mudanças não salvas serão perdidas');
      }
    return true;
  }

}
