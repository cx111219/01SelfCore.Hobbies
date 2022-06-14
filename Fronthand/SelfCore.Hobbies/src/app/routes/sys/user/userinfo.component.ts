import { Component, OnInit } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-sys-userinfo',
  templateUrl: './html/userinfo.component.html'
})
export class SysUserinfoComponent implements OnInit {
  record: any = {};
  i: any;

  constructor(private modal: NzModalRef, private msgSrv: NzMessageService, private http: _HttpClient) {}

  ngOnInit(): void {
    this.http.get(`/user/${this.record.id}`).subscribe(res => (this.i = res));
  }

  close(): void {
    this.modal.destroy();
  }
}
