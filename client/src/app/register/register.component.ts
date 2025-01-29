import { Component, EventEmitter, inject, input, Input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  //@Input() usersFromHomeComponent: any; //old way to get something from parent for template
  usersFromHomeComponent = input.required<any>(); //new way to get something for template.
  //@Output() cancelRegister = new EventEmitter(); //old way to send something to parent
  cancelRegister = output<boolean>(); //new way to send something to parent
  model: any = {}

  register(){
    this.accountService.register(this.model).subscribe({
      next: response=> {
        console.log(response);
        this.cancel();
      },
      error: err => console.log(err)
    });
  }

  cancel(){
    this.cancelRegister.emit(false); //false because we will use this to toggle registerMode to false in parent
    //old way is the same as the new way here
  }
}
