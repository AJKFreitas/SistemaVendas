import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  signinForm: FormGroup;

  constructor(
    public fb: FormBuilder,
    public authService: AuthService,
    public router: Router,
    private route: ActivatedRoute
  ) {
    this.signinForm = this.fb.group({
      email: [''],
      senha: ['']
    })
  }

  ngOnInit() {
    this.authService.doLogout();
   }

  loginUser() {
    this.authService.signIn(this.signinForm.value);
  }

}
