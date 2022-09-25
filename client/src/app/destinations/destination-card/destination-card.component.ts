import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Destination } from 'src/app/_models/destination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-destination-card',
  templateUrl: './destination-card.component.html',
  styleUrls: ['./destination-card.component.css'],

})
export class DestinationCardComponent implements OnInit {
  @Input() destination:Destination;
  user:User;

  constructor(private destinationService:DestinationsService, private router:Router,
    private toastr:ToastrService, private accountService:AccountService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
    }

  ngOnInit(): void {
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
}

  deleteDestination() {
    this.destinationService.deleteDestination(this.destination.name).subscribe({
      next:(response)=> {this.toastr.success('Destino deletado');
      console.log(response);
      this.reloadCurrentRoute();
     }, error:()=> this.reloadCurrentRoute()})
  }

}
