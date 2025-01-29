import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  imports: [
    FormsModule, 
    NgIf, 
    BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  model: any = {};
  loggedInUserName: string = "user";

  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {console.log(response);
        this.loggedInUserName = this.model.username.toString().charAt(0).toUpperCase() + this.model.username.toString().slice(1);
      },
      error: err => console.log(err)
    })
  }

  logout()
  {
    this.accountService.logout();
  }

}
