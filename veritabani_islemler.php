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
?>
