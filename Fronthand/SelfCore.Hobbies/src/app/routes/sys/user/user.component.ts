import { ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { STChange, STColumn, STComponent, STData } from '@delon/abc/st';
import { SFSchema } from '@delon/form';
import { ModalHelper, _HttpClient } from '@delon/theme';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-sys-user',
  templateUrl: './html/user.component.html'
})
export class SysUserComponent implements OnInit {
  url = `api/User`;
  query = {
    keyword: '',
    size: 10,
    page: 1
  };
  loading: boolean = false;
  data: any = [];
  selectedRows: STData[] = [];
  totalPager: number = 0;
  @ViewChild('st') private readonly st!: STComponent;
  columns: STColumn[] = [
    { title: '', index: 'id.value', type: 'checkbox' },
    { title: '编号', type: 'no' },
    { title: '头像', type: 'img', width: 60, index: 'headshot' },
    { title: '用户名', index: 'code' },
    { title: '昵称', index: 'name' },
    { title: '是否管理员', type: 'enum', index: 'isAdmin' },
    { title: '注册时间', type: 'date', index: 'creatime' },
    {
      title: '操作',
      buttons: [
        { text: '查看', click: (item: any) => `/sys/user/${item.id}` }
        // { text: '编辑', type: 'static', component: FormEditComponent, click: 'reload' },
      ]
    }
  ];

  constructor(private http: _HttpClient, private modal: ModalHelper, private modalSrv: NzModalService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.loading = true;
    this.http.get(this.url + '/pagerList', this.query).subscribe(res => {
      this.loading = false;

      this.data = res.data;
    });
  }

  info(): void {}

  // 新增 跳转页面
  add() {}

  remove() {}

  removeBatch() {}

  stChange(e: STChange): void {
    switch (e.type) {
      case 'checkbox':
        this.selectedRows = e.checkbox!;
        // this.totalCallNo = this.selectedRows.reduce((total, cv) => total + cv['callNo'], 0);
        this.cdr.detectChanges();
        break;
      case 'filter':
        this.getData();
        break;
    }
  }
}
