import { Directive, OnInit, Input, ViewContainerRef, TemplateRef } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from 'src/app/components/Auth/shared/services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {

  @Input() appHasRole: string[];
  private jwtHelper = new JwtHelperService();
  isVisible = false;
  userRoles = null;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private authService: AuthService
  ) {}

  ngOnInit() {
    if (localStorage.getItem('access_token')) {
      this.userRoles = this.jwtHelper.decodeToken(localStorage.getItem('access_token')).role as Array<string>;
    }
    // const userRoles = UsuarioService.decodedToken.role as Array<string>;   "role": "Admin"
    // if no roles clear viewcontainerref
    if (!this.userRoles) {
      this.viewContainerRef.clear();
    }

    // if user has a particular role needed then render element
    if (this.userRoles) {
      if (this.authService.roleMatch(this.appHasRole)) {
        if (!this.isVisible) {
          this.isVisible = true;
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.isVisible = false;
          this.viewContainerRef.clear();
        }
      }
    }
  }

}
