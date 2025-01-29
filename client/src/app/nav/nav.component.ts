import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  imports: [
    FormsModule, 
    BsDropdownModule,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  model: any = {};

  loggedInUserName: string = "user";

  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.loggedInUserName = 
          this.model.username.toString().charAt(0).toUpperCase() + 
          this.model.username.toString().slice(1);
        this.router.navigateByUrl('/members');
      },
      error: err => this.toastr.error(err.error)
    })
  }

  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
