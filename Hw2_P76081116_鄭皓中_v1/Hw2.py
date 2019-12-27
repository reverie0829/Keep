import sys
import cv2
import numpy as np
from Hw2_ui import Ui_MainWindow
from matplotlib import pyplot as plt
from PyQt5.QtWidgets import QMainWindow, QApplication



class MainWindow(QMainWindow, Ui_MainWindow):
    def __init__(self, parent=None):
        super(MainWindow, self).__init__(parent)
        self.setupUi(self)
        self.onBindingUI()

    def onBindingUI(self):
        self.pushButton.clicked.connect(self.on_btn1_1_click)
        self.pushButton_2.clicked.connect(self.on_btn2_1_click)
        self.pushButton_3.clicked.connect(self.on_btn3_1_click)
        self.pushButton_4.clicked.connect(self.on_btn3_2_click)

    def on_btn1_1_click(self):
        print("1.1 Disparity")
        imgL = cv2.imread('images\imL.png', 0)
        imgR = cv2.imread('images\imR.png', 0)

        stereo = cv2.StereoBM_create(numDisparities=64, blockSize=9)
        disparity = stereo.compute(imgL, imgR)
        plt.imshow(disparity, 'gray')
        plt.show()

    def on_btn2_1_click(self):
        print("2.1 NCC")
        img_rgb = cv2.imread("images\\ncc_img.jpg")
        img_gray = cv2.cvtColor(img_rgb, cv2.COLOR_BGR2GRAY)
        template = cv2.imread("images\\ncc_template.jpg", cv2.IMREAD_GRAYSCALE)
        w, h = template.shape[::-1]

        res = cv2.matchTemplate(img_gray, template, cv2.TM_CCOEFF_NORMED)
        res = cv2.normalize(res, res, 0, 1, cv2.NORM_MINMAX, -1)
        loc = np.where(res >= 0.97)

        for pt in zip(*loc[::-1]):
            cv2.rectangle(img_rgb, pt, (pt[0] + w, pt[1] + h), (255, 0, 0), 2)

        cv2.imshow('Template matching feature', res)
        cv2.imshow('Detected', img_rgb)
        #cv2.waitKey(0)
        #cv2.destroyAllWindows()

    def on_btn3_1_click(self):
        print("3.1 Keypoints")
        img1 = cv2.imread('images\Aerial1.jpg', cv2.IMREAD_GRAYSCALE)
        img2 = cv2.imread('images\Aerial2.jpg', cv2.IMREAD_GRAYSCALE)

        # Initiate SIFT detector
        sift = cv2.xfeatures2d.SIFT_create()

        # Find the keypoints and descriptors with SIFT
        kp1, des1 = sift.detectAndCompute(img1, None)
        kp2, des2 = sift.detectAndCompute(img2, None)

        # BFMatcher with default params
        bf = cv2.BFMatcher()
        matches = bf.knnMatch(des1, des2, k=2)
        matches = sorted(matches, key=lambda x: x[0].distance)
        matchesMask = [[0,0] for i in range(len(matches))]

        # Convert to BGR (can color the points later )
        img1_color = cv2.cvtColor(img1.copy(), cv2.COLOR_GRAY2BGR)
        img2_color = cv2.cvtColor(img2.copy(), cv2.COLOR_GRAY2BGR)

        # Choice 6 points
        for i, (m, n) in enumerate(matches[3:465:77]):
            matchesMask[i] = [1, 0]
            # Notice: How to get the index
            pt1 = kp1[m.queryIdx].pt
            pt2 = kp2[m.trainIdx].pt
                
                            
            # Draw pairs in green
            cv2.circle(img1_color, (int(pt1[0]),int(pt1[1])), 5, (0, 255, 0), 3)
            cv2.circle(img2_color, (int(pt2[0]),int(pt2[1])), 5, (0, 255, 0), 3)

        cv2.imshow('Figure 1', img1_color)
        cv2.imshow('Figure 2', img2_color)
        cv2.imwrite('FeatureAerial1.jpg',img1_color)
        cv2.imwrite('FeatureAerial2.jpg',img2_color)
        #cv2.waitKey(0)
        #cv2.destroyAllWindows()


    def on_btn3_2_click(self):
        print("3.2 Matched Keypoints")
        img1 = cv2.imread('images\Aerial1.jpg', cv2.IMREAD_GRAYSCALE)
        img2 = cv2.imread('images\Aerial2.jpg', cv2.IMREAD_GRAYSCALE)

        # Initiate SIFT detector
        sift = cv2.xfeatures2d.SIFT_create()

        # Find the keypoints and descriptors with SIFT
        kp1, des1 = sift.detectAndCompute(img1, None)
        kp2, des2 = sift.detectAndCompute(img2, None)

        # BFMatcher with default params
        bf = cv2.BFMatcher()
        matches = bf.knnMatch(des1, des2, k=2)
        matches = sorted(matches, key=lambda x: x[0].distance)
        matchesMask = [[0,0] for i in range(len(matches))]

        # Convert to BGR (can color the points later )
        img1_circle = cv2.cvtColor(img1.copy(), cv2.COLOR_GRAY2BGR)
        img2_circle = cv2.cvtColor(img2.copy(), cv2.COLOR_GRAY2BGR)

        # Choice 6 points
        for i, (m, n) in enumerate(matches[3:465:77]):
            matchesMask[i] = [1, 0]
            # Notice: How to get the index
            pt1 = kp1[m.queryIdx].pt
            pt2 = kp2[m.trainIdx].pt
                
                            
            # Draw pairs in green
            cv2.circle(img1_circle, (int(pt1[0]),int(pt1[1])), 5, (0, 255, 0), 3)
            cv2.circle(img2_circle, (int(pt2[0]),int(pt2[1])), 5, (0, 255, 0), 3)

        # Apply ratio test
        good = []
        for m, n in matches:
            good.append([m])

        img3 = cv2.drawMatchesKnn(img1_circle, kp1, img2_circle, kp2, good[3:465:77], None, flags=cv2.DrawMatchesFlags_NOT_DRAW_SINGLE_POINTS)
        cv2.imshow('Feature points and their corresponding points.', img3)
        #cv2.waitKey(0)
        #cv2.destroyAllWindows()


if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())
