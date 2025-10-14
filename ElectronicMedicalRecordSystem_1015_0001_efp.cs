// 代码生成时间: 2025-10-15 00:01:05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 定义电子病历系统中使用到的实体类
public class Patient {
    public int PatientId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MedicalHistory { get; set; }
}

public class MedicalRecord {
    public int RecordId { get; set; }
    public int PatientId { get; set; }
# 改进用户体验
    public DateTime DateOfVisit { get; set; }
    public string Diagnosis { get; set; }
# FIXME: 处理边界情况
    public string Treatment { get; set; }
# 扩展功能模块
    public string Medications { get; set; }

    // 导航属性，用于关联病人
    public Patient Patient { get; set; }
# NOTE: 重要实现细节
}

public class ElectronicMedicalRecordSystem {

    // 存储病人和病历数据的列表，实际应用中应使用数据库存储
    private List<Patient> patients = new List<Patient>();
    private List<MedicalRecord> medicalRecords = new List<MedicalRecord>();

    // 添加病人信息
    public void AddPatient(Patient patient) {
        if (patient == null) {
            throw new ArgumentNullException(nameof(patient), "Patient cannot be null.");
        }

        patients.Add(patient);
    }

    // 添加病历记录
    public void AddMedicalRecord(MedicalRecord record) {
        if (record == null) {
# 添加错误处理
            throw new ArgumentNullException(nameof(record), "MedicalRecord cannot be null.");
        }

        // 检查病人是否存在
        if (patients.FirstOrDefault(p => p.PatientId == record.PatientId) == null) {
            throw new ArgumentException("Patient not found for the given record.");
        }

        medicalRecords.Add(record);
    }

    // 获取病人的病历信息
    public List<MedicalRecord> GetPatientRecords(int patientId) {
        return medicalRecords.Where(record => record.PatientId == patientId).ToList();
    }

    // 更新病人信息
    public void UpdatePatient(Patient updatedPatient) {
# TODO: 优化性能
        if (updatedPatient == null) {
            throw new ArgumentNullException(nameof(updatedPatient), "Updated patient cannot be null.");
# 扩展功能模块
        }

        var patient = patients.FirstOrDefault(p => p.PatientId == updatedPatient.PatientId);
        if (patient == null) {
            throw new ArgumentException("Patient not found for update.");
# 优化算法效率
        }

        patient.Name = updatedPatient.Name;
        patient.DateOfBirth = updatedPatient.DateOfBirth;
        patient.MedicalHistory = updatedPatient.MedicalHistory;
    }

    // 删除病人信息
    public void RemovePatient(int patientId) {
        var patient = patients.FirstOrDefault(p => p.PatientId == patientId);
        if (patient == null) {
            throw new ArgumentException("Patient not found for removal.");
        }

        patients.Remove(patient);
        // 同时移除对应病人的病历记录
        medicalRecords.RemoveAll(record => record.PatientId == patientId);
    }
}
# NOTE: 重要实现细节
