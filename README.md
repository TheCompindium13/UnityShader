Here’s an updated README with a section on how to add a shader to a material and then apply it to a scene in Unity 2022:

---

# UnityShader Repository

This repository contains a collection of Unity shaders designed for various environmental effects and interactions. The shaders include custom water effects, wave simulation, realistic ocean rendering using Gerstner waves, and more. Additionally, it includes a cubemap rendering tool for generating environment maps in Unity and a fly camera for scene navigation.

## Contents

### 1. **WaveSourceController**
   - Controls the direction of multiple waves for dynamic water simulation.
   - Allows for configurable wave directions, ideal for simulating realistic water movement.

### 2. **RenderCubemapWizard**
   - A Scriptable Wizard for generating cubemaps from a camera's perspective.
   - Helps create skyboxes or environment reflections in Unity.
   - Features:
     - Select a camera to generate cubemaps.
     - Render and save cubemaps with adjustable resolution.

### 3. **FlyCamera**
   - A basic fly camera script for navigating through the scene in Unity.
   - Supports:
     - WASD for movement.
     - Shift for running speed.
     - Space to restrict movement to the X/Z axis.
   - Easy to use and customizable for various camera perspectives.

### 4. **FloatingObject**
   - A script that simulates buoyancy and object movement on water surfaces.
   - Applies physics-based buoyancy with adjustable parameters like weight, roll, and bounce.
   - Integrates with the water shader to synchronize object movement with dynamic wave heights.

### 5. **RealisticOceanGerstner Shader**
   - A custom shader for simulating ocean waves using Gerstner waves.
   - Key Features:
     - Adjustable wave height, speed, frequency, and direction.
     - Gerstner wave formulation for realistic water movement.
     - Horizontal displacement for wave sharpening using the `_Lambda` parameter.
     - Foam generation based on wave height.
     - Shader includes color control for the ocean and sky, as well as simple reflection/refraction lighting.
   - Properties:
     - Wave height, speed, and frequency.
     - Foam texture and ocean color.
     - Horizontal displacement for wave sharpening.

### 6. **Interior ShaderGraph (Fake Interior Lighting)**
   - A shader graph setup simulating the interior lighting commonly used in video games.
   - Utilizes cubemaps to create the illusion of realistic indoor lighting environments.
   - The shader uses pre-generated cubemaps to simulate environment reflections within an enclosed space.

## Setup and Usage

### 1. **Clone the Repository**
   To get started, clone the repository to your local machine:
   ```bash
   git clone https://github.com/TheCompindium13/UnityShader.git
   ```

### 2. **Using the Shaders**
   - Import the shader files into your Unity 2022 project.
   - Apply the shaders to the desired materials in your scene.
   - For `FloatingObject`, ensure your object has a Rigidbody component attached for buoyancy simulation.
   - Use the `WaveSourceController` to adjust wave directions and enhance the realism of water simulation.

### 3. **Adding a Shader to a Material and Applying it to a Scene**

   **Step 1: Create a Material with Your Shader**
   1. In the Unity Editor, go to the **Project** window.
   2. Right-click in the **Assets** folder and select **Create > Material**.
   3. Name your material (e.g., "OceanMaterial").
   4. With the material selected, go to the **Inspector** window.
   5. Click the shader drop-down menu and select your shader from the **Shader** section (e.g., `Custom/RealisticOceanGerstner`).
   
   **Step 2: Customize the Material Properties**
   - Adjust properties such as **Wave Height**, **Wave Speed**, and **Ocean Color** to customize the water appearance.
   - You can also assign textures like the **Foam Texture** to control the appearance of the foam.

   **Step 3: Apply the Material to an Object in the Scene**
   1. In the **Scene** view, select the object you want to apply the material to (e.g., a plane or a mesh representing water).
   2. In the **Inspector** window, locate the **Mesh Renderer** component.
   3. Drag and drop your material into the **Materials** section of the **Mesh Renderer**.
   
   The material will now be applied to the object, and the shader will control its visual effects based on the parameters you set.

### 4. **Cubemap Wizard**
   - Open the cubemap wizard via `Tools > Cubemap Wizard` in Unity's menu.
   - Select a camera and click "Render" to generate a cubemap from the selected camera's view.

### 5. **Fly Camera**
   - Attach the `FlyCamera` script to a GameObject with a camera.
   - Use the WASD keys for movement, shift to accelerate, and space to restrict vertical movement.

## Requirements
- Unity 2022 or higher.
- Compatible with both HDRP and standard rendering pipeline.

## Sources
- Gerstner Waves - https://antoniospg.github.io/UnityOcean/OceanSimulation.html
- Gerstner Waves - https://www.youtube.com/watch?v=kqLmhNGHmpY&pp=ygUgdW5pdHkgR2Vyc3RuZXIgV2F2ZXMgc2hhZGVyIGhsc2w%3D

- Fake Interior - https://www.youtube.com/watch?v=dUjNoIxQXAA&pp=ygUhdW5pdHkgZmFrZSBpbnRlcmlvcnMgdW5pdHkgc2hhZGVy
- Fake Interior - https://www.youtube.com/watch?v=iIPRFDreMRM&pp=ygUhdW5pdHkgZmFrZSBpbnRlcmlvcnMgdW5pdHkgc2hhZGVy
## Contributing
Feel free to submit pull requests, report bugs, or suggest new features. This project is open to contributions from the community!

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

This should provide a detailed explanation on how to apply shaders to materials and objects in Unity 2022. Let me know if any further adjustments are needed!
