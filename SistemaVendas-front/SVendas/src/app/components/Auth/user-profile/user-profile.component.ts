import { Usuario } from './../shared/models/User';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  id: string;
  currentUser: Usuario = new Usuario();
  role: string;
  roles: string[] = ['Admin', 'Funcionario', 'Fornecedor', 'Vendedor'];
  constructor(
    public authService: AuthService,
    private actRoute: ActivatedRoute
  ) {
    this.id = this.actRoute.snapshot.paramMap.get('id');
    this.buscarUsuario(this.id);
  }

  private buscarUsuario(id) {
    this.authService.getUserProfile(id).subscribe(res => {
      this.currentUser = res;
    });
  }

  ngOnInit(): void {
    this.buscarUsuario(this.id);
  }

}
