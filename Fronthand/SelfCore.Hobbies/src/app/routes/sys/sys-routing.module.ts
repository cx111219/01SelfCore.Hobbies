import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SysUserComponent } from './user/user.component';
import { SysUserEditComponent } from './user/userEdit.component';
import { SysUserInfoComponent } from './user/userInfo.component';

const routes: Routes = [
  {
    path: 'user',
    children: [{ path: '', component: SysUserComponent }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SysRoutingModule {}
