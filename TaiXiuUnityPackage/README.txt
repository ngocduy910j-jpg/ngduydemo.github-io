TaiXiu Unity Package (lightweight)
=================================
Đây là gói demo giúp bạn nhanh chóng import vào Unity và chạy thử.

Chứa:
- Assets/Scripts/TaiXiuGame.cs         (Script chính)
- Assets/Sprites/                      (Thư mục placeholder cho dice sprites)
- Assets/Sounds/                       (Thư mục placeholder cho audio)
- Assets/Scenes/                       (Bạn cần tạo scene TaiXiu.unity trong Unity)
- Assets/Fonts/                        (Bạn có thể thêm font Roboto nếu muốn)

Hướng dẫn nhanh:
1. Mở Unity, tạo project 2D.
2. Trong Project window, nhấp chuột phải -> Import Package -> Custom Package...
   (Hoặc giải nén zip này và copy folder 'Assets' vào trong thư mục project Unity)
3. Thêm Scene mới: Assets/Scenes/TaiXiu.unity (nếu bạn copy Assets folder, tạo Scene theo hướng dẫn)
4. Tạo Canvas và UI như sau:
   - TextMeshPro cần được import (Window -> TextMeshPro -> Import TMP Essentials)
   - Tạo các object UI theo hướng dẫn trong README của cuộc hội thoại.
5. Kéo 'TaiXiuGame.cs' vào một GameObject (ví dụ: GameManager), gán các tham chiếu UI.
6. Thêm 6 sprite xúc xắc (dice1..dice6) vào Assets/Sprites và kéo vào mảng diceFaces.
7. Thêm audio dice_roll.mp3 vào Assets/Sounds và gán vào diceSound (AudioSource).
8. Chạy Play và test.

Ghi chú:
- File này KHÔNG chứa tài sản (images/audio) do kích thước; bạn cần thêm sprite và audio của riêng bạn vào thư mục Sprites và Sounds.
- Nếu bạn muốn, mình có thể tạo sẵn các sprite demo và audio nhỏ rồi cập nhật gói.