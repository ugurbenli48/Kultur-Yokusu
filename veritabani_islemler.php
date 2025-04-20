<?php
$baglanti = new mysqli("localhost", "root", "", "kulturyokusu_db");

if ($baglanti->connect_errno) {
    echo "MySQL bağlantı hatası: " . $baglanti->connect_error;
    exit();
}

if (isset($_POST['xd']) && $_POST['xd'] == "kayitol") {
    $nick = $_POST['nick'];
    $kontrolSorgu = "SELECT * FROM kullanicilar WHERE nick = '$nick'";
    $kontrolSonuc = $baglanti->query($kontrolSorgu);

    if ($kontrolSonuc->num_rows > 0) {
        echo "Bu kullanıcı adı zaten var!";
    } else {
        $sorgu = "INSERT INTO kullanicilar(nick, skor) VALUES('$nick', 0)";
        $sorguSonuc = $baglanti->query($sorgu);

        if ($sorguSonuc) {
            echo "Kayıt başarıyla gerçekleşti!";
        } else {
            echo "Kayıt sırasında bir hata oluştu!";
        }
    }
}
if($_POST['xd'] =='sorular') {
	
	$sorgu="select * from sorular";
    $sorguSonuc=$baglanti->query($sorgu);

if($sorguSonuc->num_rows>0) {
	
	$butunSatirlar=array();
while($row=$sorguSonuc->fetch_object()) {

      array_push($butunSatirlar,array(
	  
	  "soru" =>$row ->soru,
	  "a_Cevab"=> $row ->a_Cevabi,
      "b_Cevab"=> $row ->b_Cevabi,
      "c_Cevab"=> $row ->c_Cevabi,
      "d_Cevab"=> $row ->d_Cevabi,
      "dogruCevab"=> $row ->dogruCevap,
     )
     );	 
}
echo json_encode(array("butunSorular"=>$butunSatirlar));
}
else { //Hata 
}
}
$baglanti->close();


?>
