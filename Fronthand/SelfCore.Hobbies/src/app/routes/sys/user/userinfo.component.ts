import { Component, Input, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-sys-userInfo',
  templateUrl: './html/userInfo.component.html',
  styleUrls: ['./html/user.component.less']
})
export class SysUserInfoComponent implements OnInit {
  user: any;
  @Input('id') id: any;
  constructor(private modal: NzModalRef, private msgSrv: NzMessageService, private http: _HttpClient) {}

  ngOnInit(): void {
    this.http.get(`api/User/single/${this.id}`).subscribe(res => (this.user = res));
  }

  close(): void {
    this.modal.destroy();
  }
}
