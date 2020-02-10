# Optical Mark Recognizer
An Optical Mark Recognizing (Image Processing) desktop application created using C#.Net &amp; Matlab for correcting exam answer sheets filled by students using Computer

# Where can I use this software?
1. Schools
2. Colleges

# What is Optical Mark Recognition?
Optical Mark Recognition is the process of capturing human marked information from physical documents into a digital form through the use of computers often using an Image Processing Library.

# Workflow

The User first loads the physical filled OMR answer sheet into the scanner. He then proceeds to press the 'scan sheet' button, which would generate a digital copy of that image. The user now presses the 'Read OMR Sheet Button'.

<p>
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/workflow/1.png" width="900">  
</p>

<p>
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/workflow/2.png" width="900">  
</p>

# Challenges in bubble Detection
The process of OMR sheet detection to bubble detection to answer checking required a very high level of accuracy. In order to address those concerns Matlab was used to verify the process of initial preprocessing and custom OMR sheets were designed to provide maximum bubble detectability.

There were multiple challenges associated with this development. The major challenge was to address the errors that could be introduced either by the scanner or the test takers. If the OMR sheet was scanned upside down and/or slant, it would be very difficult for the program to detect the filled bubbles. Apparently the test taker could have used a light pencil for filling the bubbles and this could also be significantly difficult for the program to detect. In order to address these issues, I used geometric quadrant calculations and incorporated them within the flattening phase so that the post processed image was always available as correctly oriented. Similarly, to address the light pencil problem, I used adjustments on the image to darken them, which was done during second preprocessing. This functionality in itself skyrocketed the detection rates along with ability to detect the worst case scenarios to near 100% as this software would now be able to:

- Detect upside down OMR sheet
- Detect slant OMR sheet
- Detect lightly filled OMR sheet

# Analysis - Image Preprocessing & Bubble Detection
The Pre Processing consists of three parts:

1. Pre Processing phase 1
In this phase, the OMR sheet was converted to gray scale image. Two filters were then applied to the gray scale image. The first was Invert filter and second was threshold filter. This image was now supplied for the flattening phase.

2. Image Flattening
At this phase quadrant calculations are done. A sub image is extracted which is always correctly oriented. This image is now returned back to the main algorithm for a second round of preprocessing.

3. Pre Processing phase 2
A second preprocessing same as the above is applied once again on the flattened image in order to darken up the light bubbles which the test taker might have filled. The resulting image is now returned back to the main algorithm for answer (i.e. bubble) detection.

Sample Images:
<p>  
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/1.jpg" width="900" title="Scanne Image">  
</p>

<p>
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/2.jpg" width="900" title="Bubble Detection - Tuning">  
</p>

<p>
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/4.bmp" width="900" title="Bubble Detection - Achieved correct detection ">  
</p>

<p>
  <img src="https://github.com/macwanstany/optical-mark-recognizer/blob/master/images/5.jpg" width="900" title="Question Block Detection">  
</p>
