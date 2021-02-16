import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from 'src/app/core/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private accountService: AccountService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe();
    this.loginForm.reset();
    this.loginForm.setErrors(null);
  }

}
