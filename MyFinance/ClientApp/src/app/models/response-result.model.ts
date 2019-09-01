export class ResponseResult {
  constructor(
    public IsSuccess: boolean = true,
    public Message: string = 'Done',
    public Data: any = null
  ) { }
}
