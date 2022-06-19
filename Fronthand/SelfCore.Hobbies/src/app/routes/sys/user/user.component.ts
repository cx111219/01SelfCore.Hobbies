import { ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { STChange, STColumn, STComponent, STData } from '@delon/abc/st';
import { _HttpClient } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { SysUserEditComponent } from './userEdit.component';
import { SysUserInfoComponent } from './userInfo.component';

@Component({
  selector: 'app-sys-user',
  templateUrl: './html/user.component.html'
})
export class SysUserComponent implements OnInit {
  url = `api/User/`;
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
    { title: '头像', type: 'img', width: 60, index: 'headshot' }, // , click: item => this.imgExtend(item.url)
    { title: '用户名', index: 'code' },
    { title: '昵称', index: 'name' },
    { title: '是否管理员', index: 'isAdmin', format: item => (item.isAdmin ? '是' : '否') },
    { title: '注册时间', type: 'date', index: 'creatime' },
    {
      title: '操作',
      buttons: [
        { text: '查看', click: (item: any) => this.info(item.id) },
        { text: '编辑', click: (item: any) => this.add(item.id) },
        { text: '删除', click: (item: any) => this.remove(item.id) }
      ]
    }
  ];

  constructor(private http: _HttpClient, private msg: NzMessageService, private modalSrv: NzModalService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.loading = true;
    this.http.get(this.url + 'pagerList', this.query).subscribe(res => {
      this.loading = false;

      this.data = res.data;
    });
  }

  info(id: number): void {
    if (id < 0) this.msg.warning('参数异常！');
    this.modalSrv.create({
      nzTitle: '用户详情',
      nzContent: SysUserInfoComponent,
      nzComponentParams: {
        id: id
      },
      nzFooter: null
    });
  }

  // 新增 跳转页面
  add(id?: any) {
    this.modalSrv.create({
      nzTitle: id ? '编辑用户' : '创建用户',
      nzContent: SysUserEditComponent,
      nzWidth: '60%',
      nzComponentParams: {
        id: id
      },
      nzFooter: null,
      nzOnOk: () => {
        console.log('更新');
        this.getData();
      }
    });
  }

  remove(id: any) {
    if (id) {
      this.http.delete(this.url + id).subscribe(res => {
        this.msg.success('删除成功！');
        this.getData();
        return;
      });
    } else {
      this.msg.warning('参数异常！');
    }
  }

  removeBatch() {
    if (this.selectedRows.length > 0) {
      let ids = '';
      this.selectedRows.forEach(t => (ids += t['id'] + ','));
      this.http.delete(this.url + 'delBatch/' + ids.slice(ids.length - 2, 1)).subscribe(t => {
        this.msg.success('批量删除成功！');
        this.getData();
      });
    }
  }

  // 放大图片 预留
  imgExtend(url: string) {
    if (url != null && url != '') {
      this.modalSrv.create({
        nzTitle: '查看图片',
        nzContent: `<img src="${url}" width="98%"`,
        nzWidth: '60%',
        nzFooter: null
      });
    }
  }

  stChange(e: STChange): void {
    switch (e.type) {
      case 'checkbox':
        this.selectedRows = e.checkbox!;
        this.cdr.detectChanges();
        break;
      case 'filter':
        this.getData();
        break;
    }
  }
}
