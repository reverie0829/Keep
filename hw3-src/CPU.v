// Please include verilog file if you write module in other file

module CPU(
    clk,
    rst,
    instr_read,
    instr_addr,
    instr_out,
    data_read,
    data_write,
    data_addr,
    data_in,
    data_out
);

input         clk;
input         rst;
output        instr_read;
output [31:0] instr_addr;
input  [31:0] instr_out;
output        data_read;
output [3:0]  data_write;
output [31:0] data_addr;
output [31:0] data_in;
input  [31:0] data_out;

reg instr_read;
reg [31:0] instr_addr;
reg data_read;
reg [3:0] data_write;
reg [31:0] data_addr;
reg [31:0] data_in;

reg [6:0] op;
reg [2:0] funct3;
reg [6:0] funct7;
reg [4:0] shamt;reg [6:0] shiftop;
reg [4:0] rs1;
reg [4:0] rs2;
reg [4:0] rd;
reg [11:0] imm;
reg [31:0] a;
reg [31:0] b;
reg [31:0] d;

reg [31:0]data_addr_temp;
reg [2:0] cnt;
reg  instr_read_do;
reg  data_read_do;
reg [31:0] register[0:31];// 宣告 32 個 32 位元的暫存器群組 

/* Add your design */
always@(rst)begin  //initial
instr_addr=0;
end


always@(posedge  clk or negedge clk or negedge rst)begin  // clock count
if(rst)begin  
   cnt<=0;
end
else begin
cnt<=cnt+1;
end
end

always@(posedge  clk or negedge clk)begin // instr_read control

if(cnt==3'b0)begin
instr_read=1;
end
else begin
instr_read=0;
end
end

always@(posedge  clk or negedge clk)begin  // work control
if(cnt==3'b10)begin
instr_read_do=1;
end
else begin
instr_read_do=0;
end
end

always@(posedge  clk or negedge clk)begin  // data_read control
if(cnt==3'b100)begin
data_read_do=1;
end
else begin
data_read_do=0;
end
end

always@(posedge instr_read_do)begin

data_write=4'h0;
data_in=0;

begin
//instr_read=1;
register[0]=0;//zero
op=instr_out[6:0];
if(op==7'b0110011)begin  //r type
  rs2=instr_out[24:20];
  rs1=instr_out[19:15];
  funct3=instr_out[14:12];funct7=instr_out[31:25];
  rd=instr_out[11:7];
  b=register[rs2];a=register[rs1];d=register[rd];
  case({funct7,funct3})
    10'b0000000000:
	 begin
	 d=a+b;////$write("add ");
	 end
    10'b0100000000:
	 begin
	 d=a-b;////$write("sub ");
	 end
    10'b0000000001:
	 begin
	 d=a<<b[4:0];////$write("sll ");
	 end
	 10'b0000000010:
	 begin
	 d=$signed(a)<$signed(b)?1:0;
	 //$write("slt ");
	 end
	 10'b0000000011:
	 begin
	 d=a<b?1:0;//$write("sltu ");
	 end
	 10'b0000000100:
	 begin
	 d=a^b;//$write("xor ");
	 end
	 10'b0000000101:
	 begin
	 d=a>>b[4:0];//$write("srl ");
	 end
	 10'b0100000101:
	 begin
	 d=$signed(a)>>>b[4:0];//$write("sra ");
	 end
	 10'b0000000110:
	 begin
	 d=a|b;//$write("or ");
	 end
	 10'b0000000111:
	 begin
	 d=a&b;//$write("and ");
	 end
  endcase
  register[rs2]=b;register[rs1]=a;register[rd]=d;
  instr_addr=instr_addr+4;
end
else if(op==7'b0000011)begin//i type lw
   imm=instr_out[31:20];
	funct3=instr_out[14:12];
	rd=instr_out[11:7];
	rs1=instr_out[19:15];
	a=register[rs1];d=register[rd];
   case({funct3})
	3'b010:begin
	data_addr=a+{{20{imm[11]}},imm};data_read=1;
	end
	3'b000:begin
	data_addr=a+{{20{imm[11]}},imm};data_read=1;
	//$write("LB ");
	end
	3'b001:begin
	data_addr=a+{{20{imm[11]}},imm};data_read=1;
	//$write("LH ");
	end
	3'b100:begin
	data_addr=a+{{20{imm[11]}},imm};data_read=1;
	//$write("LBU ");
	end
	3'b101:begin
	data_addr=a+{{20{imm[11]}},imm};data_read=1;
	//$write("LHU ");
	end
	
	endcase

	instr_addr=instr_addr+4;
end
else if(op==7'b0100011)begin//s type sw
   rs2=instr_out[24:20];
   rs1=instr_out[19:15];
   funct3=instr_out[14:12];
   imm={instr_out[31:25],instr_out[11:7]};
	b=register[rs2];a=register[rs1];
	case({funct3})
	3'b010:begin
	data_addr=a+{{20{imm[11]}},imm};data_write=4'hf;
	data_in=b;
	//$write("sw ");
	end
	3'b000:begin
	data_addr_temp={{20{imm[11]}},imm};
	if(data_addr_temp[15:0]-data_addr_temp[15:2]*4=='d3) begin //if({{20{imm[11]}},imm}==-'d13) begin
	data_addr=a+{{20{imm[11]}},imm};data_write=4'b1000;
	data_in={b[7:0],24'b0};
	end
	else begin
	data_addr=a+{{20{imm[11]}},imm};data_write=4'b0001;
	data_in=b;
	end
	end
	3'b001:begin
	data_addr_temp={{20{imm[11]}},imm};
	if(data_addr_temp[15:0]-data_addr_temp[15:2]*4=='d2) begin  //if({{20{imm[11]}},imm}==-'d18) begin
	data_addr=a+{{20{imm[11]}},imm};data_write=4'b1100;
	data_in={b[15:0],16'b0};
	end
	else begin
	data_addr=a+{{20{imm[11]}},imm};data_write=4'b0011;
	data_in=b;
	end
	end
	
	endcase

	instr_addr=instr_addr+4;
end
else if(op==7'b1100111)begin//i type jalr
   funct3=instr_out[14:12];
	imm=instr_out[31:20];
	rd=instr_out[11:7];
	rs1=instr_out[19:15];
	a=register[rs1];d=register[rd];
	case({funct3})
	3'b000:begin
	d=instr_addr+4;instr_addr={{20{imm[11]}},imm}+a;instr_addr[0]=0;
	////$write("jalr ");
	end
	endcase
	register[rd]=d;
end
else if(op==7'b0010011)begin//i type
   imm=instr_out[31:20];shiftop=instr_out[31:25];shamt=instr_out[24:20];
	funct3=instr_out[14:12];
	rd=instr_out[11:7];
	rs1=instr_out[19:15];
	a=register[rs1];d=register[rd];
   case({funct3})
	 3'b000:begin
	 d=a+{{20{imm[11]}},imm};
	 //$write("addi ");
	 end
	 3'b010:begin
	 d=($signed(a)<$signed({{20{imm[11]}},imm}))?1:0;
	 //$write("slti ");
	 end
	 3'b011:begin
	 d=a<{{20{imm[11]}},imm}?1:0;
	 //$write("sltiu ");
	 end
	 3'b100:begin
	 d=a^{{20{imm[11]}},imm};
	 //$write("xori ");
	 end
	 3'b110:begin
	 d=a|{{20{imm[11]}},imm};
	 //$write("ori ");
	 end
	 3'b111:begin
	 d=a&{{20{imm[11]}},imm};
	 //$write("andi ");
	 end
    3'b001:begin
	 d=a<<shamt;
	 //$write("slli ");
	 end
	 3'b101:begin
	   if(shiftop==0)begin
		d=a>>shamt;//$write("srli ");
	   end
		else begin
		d=$signed(a)>>>shamt;//$write("srai ");
		end
	 end
	endcase
	register[rd]=d;

	instr_addr=instr_addr+4;
end
else if(op==7'b1100011)begin//b type
  rs2=instr_out[24:20];
  rs1=instr_out[19:15];
  funct3=instr_out[14:12];
  b=register[rs2];a=register[rs1];

  case(funct3)
  3'b000:begin
   instr_addr=(a==b)?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("beq ");
  end
  3'b001:begin
   instr_addr=(a!=b)?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("bne ");
  end
  3'b100:begin
   instr_addr=($signed(a)<$signed(b))?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("blt ");
  end
  3'b101:begin
   instr_addr=($signed(a)>=$signed(b))?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("bge ");
  end
  3'b110:begin
   instr_addr=(a<b)?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("bltu ");
  end
  3'b111:begin
   instr_addr=(a>=b)?instr_addr+{{19{instr_out[31]}},instr_out[31],instr_out[7],instr_out[30:25],instr_out[11:8],1'b0}:instr_addr+4;
   //$write("bgeu ");
  end
  endcase

  
end
else if(op==7'b0010111)begin// u type AUIPC 
   rd=instr_out[11:7];d=register[rd];
	d=instr_addr+{instr_out[31:12],12'b0};
	register[rd]=d;
   //$write("auipc\n");
	instr_addr=instr_addr+4;
end
else if(op==7'b0110111)begin//u type LUI
   rd=instr_out[11:7];d=register[rd];
	d={instr_out[31:12],12'b0};
	register[rd]=d;
	//$write("lui\n");
	instr_addr=instr_addr+4;
end
else if(op==7'b1101111)begin//j type jal
   rd=instr_out[11:7];d=register[rd];
	d=instr_addr+4;
	instr_addr=instr_addr+{{11{instr_out[31]}},instr_out[31],instr_out[19:12],instr_out[20],instr_out[30:21],1'b0};
	register[rd]=d;

end
else begin
   instr_addr=instr_addr+4;
end

end
end





always@(posedge data_read_do)begin

if(data_read)begin
	case({funct3})
		3'b010:begin 
		register[rd]=data_out;end
		3'b000:begin
		register[rd]=$signed(data_out[7:0]);end
		3'b001:begin
		register[rd]=$signed(data_out[15:0]);end
		3'b100:begin
		register[rd]=data_out[7:0];end
		3'b101:begin
		register[rd]=data_out[15:0];end
	endcase
data_read=0;
end
end

endmodule
